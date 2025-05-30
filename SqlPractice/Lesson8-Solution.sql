WITH civilization_trade_history AS (
    SELECT 
        c.civilization_type,
        EXTRACT(YEAR FROM c.arrival_date) AS trade_year,
        COUNT(DISTINCT c.caravan_id) AS caravan_count,
        SUM(tt.value) AS total_trade_value,
        SUM(CASE WHEN cg.type = 'Import' THEN cg.value ELSE 0 END) AS import_value,
        SUM(CASE WHEN cg.type = 'Export' THEN cg.value ELSE 0 END) AS export_value,
        COUNT(DISTINCT cg.goods_id) AS unique_goods_traded,
        COUNT(DISTINCT CASE WHEN cg.type = 'Import' THEN cg.goods_id END) AS unique_imports,
        COUNT(DISTINCT CASE WHEN cg.type = 'Export' THEN cg.goods_id END) AS unique_exports
    FROM 
        caravans c
    JOIN 
        trade_transactions tt ON c.caravan_id = tt.caravan_id
    JOIN 
        caravan_goods cg ON c.caravan_id = cg.caravan_id
    GROUP BY 
        c.civilization_type, EXTRACT(YEAR FROM c.arrival_date)
),
fortress_resource_dependency AS (
    SELECT 
        cg.material_type,
        COUNT(DISTINCT cg.goods_id) AS times_imported,
        SUM(cg.quantity) AS total_imported,
        SUM(cg.value) AS total_import_value,
        COUNT(DISTINCT c.caravan_id) AS caravans_importing,
        AVG(cg.price_fluctuation) AS avg_price_fluctuation,
        -- Calculate resource dependency score
        (COUNT(DISTINCT cg.goods_id) * 
         SUM(cg.quantity) * 
         (1.0 / NULLIF(COUNT(DISTINCT c.civilization_type), 0))
        ) AS dependency_score
    FROM 
        caravan_goods cg
    JOIN 
        caravans c ON cg.caravan_id = c.caravan_id
    WHERE 
        cg.type = 'Import'
    GROUP BY 
        cg.material_type
),
diplomatic_trade_correlation AS (
    SELECT 
        c.civilization_type,
        COUNT(DISTINCT de.event_id) AS diplomatic_events,
        COUNT(DISTINCT CASE WHEN de.outcome = 'Positive' THEN de.event_id END) AS positive_events,
        COUNT(DISTINCT CASE WHEN de.outcome = 'Negative' THEN de.event_id END) AS negative_events,
        SUM(tt.value) AS total_trade_value,
        CORR(
            de.relationship_change,
            tt.value
        ) AS trade_diplomacy_correlation
    FROM 
        caravans c
    JOIN 
        trade_transactions tt ON c.caravan_id = tt.caravan_id
    JOIN 
        diplomatic_events de ON c.civilization_type = de.civilization_type
    GROUP BY 
        c.civilization_type
),
workshop_export_effectiveness AS (
    SELECT 
        p.type AS product_type,
        w.type AS workshop_type,
        COUNT(DISTINCT p.product_id) AS products_created,
        COUNT(DISTINCT CASE WHEN cg.goods_id IS NOT NULL THEN p.product_id END) AS products_exported,
        SUM(p.value) AS total_production_value,
        SUM(CASE WHEN cg.goods_id IS NOT NULL THEN cg.value ELSE 0 END) AS export_value,
        AVG(CASE WHEN cg.goods_id IS NOT NULL THEN (cg.value / p.value) ELSE NULL END) AS avg_export_markup
    FROM 
        products p
    JOIN 
        workshops w ON p.workshop_id = w.workshop_id
    LEFT JOIN 
        caravan_goods cg ON p.product_id = cg.original_product_id AND cg.type = 'Export'
    GROUP BY 
        p.type, w.type
),
trade_timeline AS (
    SELECT 
        EXTRACT(YEAR FROM c.arrival_date) AS year,
        EXTRACT(QUARTER FROM c.arrival_date) AS quarter,
        SUM(tt.value) AS quarterly_trade_value,
        COUNT(DISTINCT c.civilization_type) AS trading_civilizations,
        SUM(CASE WHEN tt.balance_direction = 'Import' THEN tt.value ELSE 0 END) AS import_value,
        SUM(CASE WHEN tt.balance_direction = 'Export' THEN tt.value ELSE 0 END) AS export_value,
        LAG(SUM(tt.value)) OVER (ORDER BY EXTRACT(YEAR FROM c.arrival_date), EXTRACT(QUARTER FROM c.arrival_date)) AS previous_quarter_value
    FROM 
        caravans c
    JOIN 
        trade_transactions tt ON c.caravan_id = tt.caravan_id
    GROUP BY 
        EXTRACT(YEAR FROM c.arrival_date), EXTRACT(QUARTER FROM c.arrival_date)
)
SELECT 
    -- Overall trade statistics
    (SELECT COUNT(DISTINCT civilization_type) FROM caravans) AS total_trading_partners,
    (SELECT SUM(total_trade_value) FROM civilization_trade_history) AS all_time_trade_value,
    (SELECT SUM(export_value) - SUM(import_value) FROM civilization_trade_history) AS all_time_trade_balance,
    
    -- Civilization breakdown with REST API format
    JSON_OBJECT(
        'civilization_trade_data', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'civilization_type', cth.civilization_type,
                    'total_caravans', SUM(cth.caravan_count),
                    'total_trade_value', SUM(cth.total_trade_value),
                    'trade_balance', SUM(cth.export_value) - SUM(cth.import_value),
                    'trade_relationship', CASE 
                        WHEN (SUM(cth.export_value) - SUM(cth.import_value)) > 0 THEN 'Favorable'
                        WHEN (SUM(cth.export_value) - SUM(cth.import_value)) < 0 THEN 'Unfavorable'
                        ELSE 'Balanced' 
                    END,
                    'diplomatic_correlation', dtc.trade_diplomacy_correlation,
                    'unique_goods_traded', SUM(cth.unique_goods_traded),
                    'years_active', COUNT(DISTINCT cth.trade_year),
                    'caravan_ids', (
                        SELECT JSON_ARRAYAGG(c.caravan_id)
                        FROM caravans c
                        WHERE c.civilization_type = cth.civilization_type
                    )
                )
            )
            FROM civilization_trade_history cth
            LEFT JOIN diplomatic_trade_correlation dtc ON cth.civilization_type = dtc.civilization_type
            GROUP BY cth.civilization_type, dtc.trade_diplomacy_correlation
        )
    ) AS civilization_data,
    
    -- Resource dependency analysis
    JSON_OBJECT(
        'resource_dependency', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'material_type', frd.material_type,
                    'dependency_score', frd.dependency_score,
                    'total_imported', frd.total_imported,
                    'import_diversity', frd.caravans_importing,
                    'price_volatility', frd.avg_price_fluctuation,
                    'resource_ids', (
                        SELECT JSON_ARRAYAGG(DISTINCT r.resource_id)
                        FROM resources r
                        JOIN caravan_goods cg ON r.name = cg.material_type
                        WHERE r.type = frd.material_type
                    )
                )
            )
            FROM fortress_resource_dependency frd
            ORDER BY frd.dependency_score DESC
            LIMIT 10
        )
    ) AS critical_import_dependencies,
    
    -- Workshop export analysis
    JSON_OBJECT(
        'export_effectiveness', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'workshop_type', wee.workshop_type,
                    'product_type', wee.product_type,
                    'export_ratio', ROUND((wee.products_exported::DECIMAL / NULLIF(wee.products_created, 0)) * 100, 2),
                    'avg_markup', wee.avg_export_markup,
                    'total_export_value', wee.export_value,
                    'workshop_ids', (
                        SELECT JSON_ARRAYAGG(w.workshop_id)
                        FROM workshops w
                        WHERE w.type = wee.workshop_type
                    )
                )
            )
            FROM workshop_export_effectiveness wee
            WHERE wee.products_created > 0
            ORDER BY wee.export_value DESC
        )
    ) AS export_effectiveness,
    
    -- Trade growth analysis
    JSON_OBJECT(
        'trade_growth', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'year', tt.year,
                    'quarter', tt.quarter,
                    'quarterly_value', tt.quarterly_trade_value,
                    'quarterly_balance', tt.export_value - tt.import_value,
                    'growth_from_previous', CASE 
                        WHEN tt.previous_quarter_value IS NULL THEN NULL
                        ELSE ROUND(((tt.quarterly_trade_value - tt.previous_quarter_value) / 
                              NULLIF(tt.previous_quarter_value, 0)) * 100, 2)
                    END,
                    'trade_diversity', tt.trading_civilizations
                )
            )
            FROM trade_timeline tt
            ORDER BY tt.year, tt.quarter
        )
    ) AS trade_timeline,
    
    -- Trade impact on fortress economy
    JSON_OBJECT(
        'economic_impact', (
            SELECT JSON_OBJECT(
                'import_to_production_ratio', ROUND(
                    (SELECT SUM(import_value) FROM civilization_trade_history) /
                    NULLIF((SELECT SUM(total_production_value) FROM workshop_export_effectiveness), 0) * 100, 2
                ),
                'export_to_production_ratio', ROUND(
                    (SELECT SUM(export_value) FROM civilization_trade_history) /
                    NULLIF((SELECT SUM(total_production_value) FROM workshop_export_effectiveness), 0) * 100, 2
                ),
                'trade_dependency_score', ROUND(
                    (SELECT SUM(total_trade_value) FROM civilization_trade_history) /
                    (SELECT COUNT(DISTINCT trade_year) FROM civilization_trade_history) /
                    (SELECT SUM(p.value) FROM products p) * 100, 2
                ),
                'most_profitable_exports', (
                    SELECT JSON_ARRAYAGG(
                        JSON_OBJECT(
                            'product_type', x.product_type,
                            'total_value', x.export_value,
                            'product_ids', (
                                SELECT JSON_ARRAYAGG(p.product_id)
                                FROM products p
                                JOIN caravan_goods cg ON p.product_id = cg.original_product_id
                                WHERE p.type = x.product_type AND cg.type = 'Export'
                                LIMIT 100
                            )
                        )
                    )
                    FROM (
                        SELECT product_type, SUM(export_value) AS export_value
                        FROM workshop_export_effectiveness
                        GROUP BY product_type
                        ORDER BY SUM(export_value) DESC
                        LIMIT 5
                    ) x
                ),
                'most_expensive_imports', (
                    SELECT JSON_ARRAYAGG(
                        JSON_OBJECT(
                            'material_type', i.material_type,
                            'total_value', i.import_value,
                            'goods_ids', (
                                SELECT JSON_ARRAYAGG(cg.goods_id)
                                FROM caravan_goods cg
                                WHERE cg.material_type = i.material_type AND cg.type = 'Import'
                                LIMIT 100
                            )
                        )
                    )
                    FROM (
                        SELECT 
                            cg.material_type,
                            SUM(cg.value) AS import_value
                        FROM caravan_goods cg
                        WHERE cg.type = 'Import'
                        GROUP BY cg.material_type
                        ORDER BY SUM(cg.value) DESC
                        LIMIT 5
                    ) i
                )
            )
        )
    ) AS economic_impact,
    
    -- Recommendations based on trade analysis
    JSON_OBJECT(
        'trade_recommendations', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'recommendation_type', 
                    CASE
                        WHEN r.dependency_score > 1000 THEN 'Critical Dependency'
                        WHEN r.dependency_score > 500 THEN 'High Dependency'
                        WHEN r.dependency_score > 100 THEN 'Moderate Dependency'
                        ELSE 'Low Dependency'
                    END,
                    'material_type', r.material_type,
                    'recommended_action', 
                    CASE
                        WHEN r.dependency_score > 1000 THEN 'Develop domestic production'
                        WHEN r.dependency_score > 500 THEN 'Diversify import sources'
                        WHEN r.dependency_score > 100 THEN 'Maintain strategic reserves'
                        ELSE 'Continue current trade strategy'
                    END,
                    'potential_partners', (
                        SELECT JSON_ARRAYAGG(DISTINCT c.civilization_type)
                        FROM caravans c
                        JOIN caravan_goods cg ON c.caravan_id = cg.caravan_id
                        WHERE cg.material_type = r.material_type AND cg.type = 'Import'
                    ),
                    'resource_ids', (
                        SELECT JSON_ARRAYAGG(DISTINCT r2.resource_id)
                        FROM resources r2
                        WHERE r2.type = r.material_type
                    )
                )
            )
            FROM fortress_resource_dependency r
            ORDER BY r.dependency_score DESC
            LIMIT 10
        ),
        'export_opportunities', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'workshop_type', w.type,
                    'current_export_ratio', COALESCE(
                        (SELECT ROUND((wee.products_exported::DECIMAL / NULLIF(wee.products_created, 0)) * 100, 2)
                         FROM workshop_export_effectiveness wee
                         WHERE wee.workshop_type = w.type
                         LIMIT 1),
                        0
                    ),
                    'potential_value', COALESCE(
                        (SELECT SUM(p.value)
                         FROM products p
                         JOIN workshops w2 ON p.workshop_id = w2.workshop_id
                         LEFT JOIN caravan_goods cg ON p.product_id = cg.original_product_id AND cg.type = 'Export'
                         WHERE w2.type = w.type AND cg.goods_id IS NULL),
                        0
                    ),
                    'recommended_civilizations', (
                        SELECT JSON_ARRAYAGG(DISTINCT c.civilization_type)
                        FROM caravans c
                        JOIN caravan_goods cg ON c.caravan_id = cg.caravan_id
                        WHERE cg.type = 'Import' AND c.civilization_type IN (
                            SELECT DISTINCT c2.civilization_type
                            FROM caravans c2
                            JOIN trade_transactions tt ON c2.caravan_id = tt.caravan_id
                            JOIN diplomatic_events de ON c2.civilization_type = de.civilization_type
                            WHERE de.outcome = 'Positive'
                        )
                    ),
                    'workshop_ids', (
                        SELECT JSON_ARRAYAGG(w2.workshop_id)
                        FROM workshops w2
                        WHERE w2.type = w.type
                    )
                )
            )
            FROM (
                SELECT DISTINCT type
                FROM workshops
            ) w
        )
    ) AS trade_recommendations
FROM (SELECT 1) AS dummy;