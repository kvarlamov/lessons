/*
Задача 4*: Комплексный анализ торговых отношений и их влияния на крепость

Разработайте запрос, анализирующий торговые отношения со всеми цивилизациями, оценивая:
- Баланс торговли с каждой цивилизацией за все время
- Влияние товаров каждого типа на экономику крепости
- Корреляцию между торговлей и дипломатическими отношениями
- Эволюцию торговых отношений во времени
- Зависимость крепости от определенных импортируемых товаров
- Эффективность экспорта продукции мастерских

Возможный вариант выдачи:


{
  "total_trading_partners": 5,
  "all_time_trade_value": 15850000,
  "all_time_trade_balance": 1250000,
  "civilization_data": {
    "civilization_trade_data": [
      {
        "civilization_type": "Human",
        "total_caravans": 42,
        "total_trade_value": 5240000,
        "trade_balance": 840000,
        "trade_relationship": "Favorable",
        "diplomatic_correlation": 0.78,
        "caravan_ids": [1301, 1305, 1308, 1312, 1315]
      },
      {
        "civilization_type": "Elven",
        "total_caravans": 38,
        "total_trade_value": 4620000,
        "trade_balance": -280000,
        "trade_relationship": "Unfavorable",
        "diplomatic_correlation": 0.42,
        "caravan_ids": [1302, 1306, 1309, 1316, 1322]
      }
    ]
  },
  "critical_import_dependencies": {
    "resource_dependency": [
      {
        "material_type": "Exotic Metals",
        "dependency_score": 2850.5,
        "total_imported": 5230,
        "import_diversity": 4,
        "resource_ids": [202, 208, 215]
      },
      {
        "material_type": "Lumber",
        "dependency_score": 1720.3,
        "total_imported": 12450,
        "import_diversity": 3,
        "resource_ids": [203, 209, 216]
      }
    ]
  },
  "export_effectiveness": {
    "export_effectiveness": [
      {
        "workshop_type": "Smithy",
        "product_type": "Weapons",
        "export_ratio": 78.5,
        "avg_markup": 1.85,
        "workshop_ids": [301, 305, 310]
      },
      {
        "workshop_type": "Jewelery",
        "product_type": "Ornaments",
        "export_ratio": 92.3,
        "avg_markup": 2.15,
        "workshop_ids": [304, 309, 315]
      }
    ]
  },
  "trade_timeline": {
    "trade_growth": [
      {
        "year": 205,
        "quarter": 1,
        "quarterly_value": 380000,
        "quarterly_balance": 20000,
        "trade_diversity": 3
      },
      {
        "year": 205,
        "quarter": 2,
        "quarterly_value": 420000,
        "quarterly_balance": 35000,
        "trade_diversity": 4
      }
    ]
  }
}
*/


WITH TradeSummary AS (
    SELECT 
        c.civilization_type,
        COUNT(DISTINCT c.caravan_id) AS total_caravans,
        SUM(t.value) AS total_trade_value,
        SUM(CASE WHEN t.balance_direction = 'Export' THEN t.value ELSE -t.value END) AS trade_balance,
        ARRAY_AGG(c.caravan_id) AS caravan_ids
    FROM CARAVANS c
    JOIN TRADE_TRANSACTIONS t ON c.caravan_id = t.caravan_id
    GROUP BY c.civilization_type
),
DiplomaticCorrelation AS (
    SELECT 
        c.civilization_type,
        AVG(de.relationship_change) AS diplomatic_correlation
    FROM CARAVANS c
    JOIN DIPLOMATIC_EVENTS de ON c.caravan_id = de.caravan_id
    GROUP BY c.civilization_type
),
ImportDependency AS (
    SELECT 
        cg.material_type,
        SUM(cg.quantity) AS total_imported,
        COUNT(DISTINCT c.civilization_type) AS import_diversity,
        ARRAY_AGG(cg.resource_id) AS resource_ids,
        SUM(cg.quantity * cg.value) / NULLIF(SUM(fr.quantity), 0) AS dependency_score
    FROM CARAVAN_GOODS cg
    JOIN CARAVANS c ON cg.caravan_id = c.caravan_id
    LEFT JOIN FORTRESS_RESOURCES fr ON cg.resource_id = fr.resource_id
    WHERE cg.type = 'Import'
    GROUP BY cg.material_type
    HAVING SUM(fr.quantity) IS NULL OR SUM(fr.quantity) = 0
),
ExportEffectiveness AS (
    SELECT 
        w.type AS workshop_type,
        p.type AS product_type,
        COUNT(DISTINCT w.workshop_id) AS workshop_count,
        ARRAY_AGG(w.workshop_id) AS workshop_ids,
        100.0 * SUM(CASE WHEN cg.type = 'Export' THEN wp.quantity ELSE 0 END) / SUM(wp.quantity) AS export_ratio,
        AVG(CASE WHEN cg.type = 'Export' THEN cg.value / NULLIF(p.value, 0) ELSE NULL END) AS avg_markup
    FROM WORKSHOP_PRODUCTS wp
    JOIN PRODUCTS p ON wp.product_id = p.product_id
    JOIN WORKSHOPS w ON wp.workshop_id = w.workshop_id
    LEFT JOIN CARAVAN_GOODS cg ON p.product_id = cg.original_product_id
    GROUP BY w.type, p.type
    HAVING SUM(CASE WHEN cg.type = 'Export' THEN wp.quantity ELSE 0 END) > 0
),
TradeTimeline AS (
    SELECT 
        EXTRACT(YEAR FROM t.date) AS year,
        EXTRACT(QUARTER FROM t.date) AS quarter,
        SUM(t.value) AS quarterly_value,
        SUM(CASE WHEN t.balance_direction = 'Export' THEN t.value ELSE -t.value END) AS quarterly_balance,
        COUNT(DISTINCT c.civilization_type) AS trade_diversity
    FROM TRADE_TRANSACTIONS t
    JOIN CARAVANS c ON t.caravan_id = c.caravan_id
    GROUP BY EXTRACT(YEAR FROM t.date), EXTRACT(QUARTER FROM t.date)
    ORDER BY year, quarter
)
SELECT 
    JSON_OBJECT(
        'total_trading_partners', (SELECT COUNT(DISTINCT civilization_type) FROM CARAVANS),
        'all_time_trade_value', (SELECT SUM(value) FROM TRADE_TRANSACTIONS),
        'all_time_trade_balance', (SELECT SUM(CASE WHEN balance_direction = 'Export' THEN value ELSE -value END) FROM TRADE_TRANSACTIONS),
        'civilization_data', JSON_OBJECT(
            'civilization_trade_data', (
                SELECT JSON_AGG(
                    JSON_OBJECT(
                        'civilization_type', ts.civilization_type,
                        'total_caravans', ts.total_caravans,
                        'total_trade_value', ts.total_trade_value,
                        'trade_balance', ts.trade_balance,
                        'trade_relationship', CASE 
                            WHEN ts.trade_balance > 0 THEN 'Favorable'
                            WHEN ts.trade_balance < 0 THEN 'Unfavorable'
                            ELSE 'Neutral'
                        END,
                        'diplomatic_correlation', dc.diplomatic_correlation,
                        'caravan_ids', ts.caravan_ids
                    )
                )
                FROM TradeSummary ts
                LEFT JOIN DiplomaticCorrelation dc ON ts.civilization_type = dc.civilization_type
            )
        ),
        'critical_import_dependencies', JSON_OBJECT(
            'resource_dependency', (
                SELECT JSON_AGG(
                    JSON_OBJECT(
                        'material_type', id.material_type,
                        'dependency_score', id.dependency_score,
                        'total_imported', id.total_imported,
                        'import_diversity', id.import_diversity,
                        'resource_ids', id.resource_ids
                    )
                )
                FROM ImportDependency id
            )
        ),
        'export_effectiveness', JSON_OBJECT(
            'export_effectiveness', (
                SELECT JSON_AGG(
                    JSON_OBJECT(
                        'workshop_type', ee.workshop_type,
                        'product_type', ee.product_type,
                        'export_ratio', ROUND(ee.export_ratio, 1),
                        'avg_markup', ROUND(ee.avg_markup, 2),
                        'workshop_ids', ee.workshop_ids
                    )
                )
                FROM ExportEffectiveness ee
            )
        ),
        'trade_timeline', JSON_OBJECT(
            'trade_growth', (
                SELECT JSON_AGG(
                    JSON_OBJECT(
                        'year', tt.year,
                        'quarter', tt.quarter,
                        'quarterly_value', tt.quarterly_value,
                        'quarterly_balance', tt.quarterly_balance,
                        'trade_diversity', tt.trade_diversity
                    )
                )
                FROM TradeTimeline tt
            )
        )
    ) AS trade_analysis;