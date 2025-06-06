WITH creature_attack_history AS (
    SELECT 
        ca.attack_id,
        ca.creature_id,
        c.type AS creature_type,
        c.threat_level,
        ca.date,
        ca.location_id,
        ca.outcome,
        ca.casualties AS fortress_casualties,
        ca.enemy_casualties,
        ca.defense_structures_used,
        ca.military_response_time_minutes,
        EXTRACT(YEAR FROM ca.date) AS year,
        EXTRACT(MONTH FROM ca.date) AS month,
        EXTRACT(DOW FROM ca.date) AS day_of_week,
        l.zone_type,
        l.depth,
        l.fortification_level
    FROM 
        creature_attacks ca
    JOIN 
        creatures c ON ca.creature_id = c.creature_id
    JOIN 
        locations l ON ca.location_id = l.location_id
),
defense_structure_effectiveness AS (
    SELECT 
        ds.structure_id,
        ds.type AS defense_type,
        ds.location_id,
        l.zone_type,
        COUNT(DISTINCT ca.attack_id) AS attacks_defended,
        SUM(CASE WHEN ca.outcome = 'Repelled' THEN 1 ELSE 0 END) AS successful_defenses,
        AVG(ca.enemy_casualties) AS avg_enemy_casualties,
        ds.construction_date,
        ds.last_maintenance_date,
        EXTRACT(DAY FROM (CURRENT_DATE - ds.last_maintenance_date)) AS days_since_maintenance,
        ds.quality,
        ds.material_id
    FROM 
        defense_structures ds
    JOIN 
        locations l ON ds.location_id = l.location_id
    LEFT JOIN 
        creature_attacks ca ON ds.structure_id = ANY(ca.defense_structures_used)
    GROUP BY 
        ds.structure_id, ds.type, ds.location_id, l.zone_type,
        ds.construction_date, ds.last_maintenance_date, ds.quality, ds.material_id
),
zone_vulnerability AS (
    SELECT 
        l.zone_id,
        l.name AS zone_name,
        l.zone_type,
        l.depth,
        l.access_points,
        l.fortification_level,
        COUNT(DISTINCT ca.attack_id) AS total_attacks,
        SUM(CASE WHEN ca.outcome = 'Breached' THEN 1 ELSE 0 END) AS breaches,
        AVG(ca.fortress_casualties) AS avg_casualties_per_attack,
        COUNT(DISTINCT ds.structure_id) AS defense_structures,
        COUNT(DISTINCT CASE WHEN sm.patrol_zone_id = l.zone_id THEN sm.squad_id END) AS patrolling_squads,
        -- Distance to nearest military response
        MIN(ms.response_time_to_zone) AS min_response_time,
        -- Structural metrics
        l.wall_integrity,
        l.trap_density,
        l.choke_points
    FROM 
        locations l
    LEFT JOIN 
        creature_attacks ca ON l.zone_id = ca.location_id
    LEFT JOIN 
        defense_structures ds ON l.zone_id = ds.location_id
    LEFT JOIN 
        squad_movement sm ON l.zone_id = sm.patrol_zone_id
    LEFT JOIN 
        military_stations ms ON l.zone_id = ms.coverage_zone_id
    GROUP BY 
        l.zone_id, l.name, l.zone_type, l.depth, l.access_points,
        l.fortification_level, l.wall_integrity, l.trap_density, l.choke_points
),
seasonal_attack_patterns AS (
    SELECT 
        EXTRACT(YEAR FROM ca.date) AS year,
        EXTRACT(MONTH FROM ca.date) AS month,
        EXTRACT(SEASON FROM ca.date) AS season,
        COUNT(DISTINCT ca.attack_id) AS attack_count,
        COUNT(DISTINCT ca.creature_id) AS unique_attackers,
        AVG(c.threat_level) AS avg_threat_level,
        SUM(ca.fortress_casualties) AS total_casualties,
        SUM(ca.enemy_casualties) AS total_enemy_casualties,
        -- Weather correlation
        w.temperature,
        w.precipitation,
        w.weather_event_type,
        -- Moon phase correlation
        mp.phase AS moon_phase,
        -- Average defense effectiveness during this period
        AVG(CASE 
            WHEN ca.outcome = 'Repelled' THEN 1.0
            WHEN ca.outcome = 'Partial Breach' THEN 0.5
            WHEN ca.outcome = 'Breached' THEN 0.0
        END) AS avg_defense_effectiveness
    FROM 
        creature_attacks ca
    JOIN 
        creatures c ON ca.creature_id = c.creature_id
    JOIN 
        weather_records w ON DATE_TRUNC('day', ca.date) = w.date
    JOIN 
        moon_phases mp ON DATE_TRUNC('day', ca.date) = mp.date
    GROUP BY 
        EXTRACT(YEAR FROM ca.date),
        EXTRACT(MONTH FROM ca.date),
        EXTRACT(SEASON FROM ca.date),
        w.temperature, w.precipitation, w.weather_event_type,
        mp.phase
),
military_readiness AS (
    SELECT 
        ms.squad_id,
        s.name AS squad_name,
        s.formation_type,
        ms.station_id,
        ms.current_location_id,
        l.zone_type,
        ms.alert_level,
        ms.readiness_score,
        COUNT(DISTINCT sm.dwarf_id) AS active_members,
        AVG(ds.level) AS avg_combat_skill,
        MIN(eq.quality) AS min_equipment_quality,
        AVG(eq.quality) AS avg_equipment_quality,
        COUNT(DISTINCT ca.attack_id) AS battles_participated,
        SUM(CASE WHEN ca.outcome IN ('Repelled', 'Partial Breach') THEN 1 ELSE 0 END) AS successful_defenses,
        AVG(ms.response_time_to_zone) AS avg_response_time,
        MAX(ms.last_training_date) AS last_training,
        EXTRACT(DAY FROM (CURRENT_DATE - MAX(ms.last_training_date))) AS days_since_training
    FROM 
        military_stations ms
    JOIN 
        military_squads s ON ms.squad_id = s.squad_id
    JOIN 
        locations l ON ms.current_location_id = l.location_id
    JOIN 
        squad_members sm ON s.squad_id = sm.squad_id AND sm.active = TRUE
    JOIN 
        dwarf_skills ds ON sm.dwarf_id = ds.dwarf_id AND ds.skill_type = 'Combat'
    JOIN 
        dwarf_equipment de ON sm.dwarf_id = de.dwarf_id
    JOIN 
        equipment eq ON de.equipment_id = eq.equipment_id
    LEFT JOIN 
        squad_battle_participation sbp ON s.squad_id = sbp.squad_id
    LEFT JOIN 
        creature_attacks ca ON sbp.attack_id = ca.attack_id
    GROUP BY 
        ms.squad_id, s.name, s.formation_type, ms.station_id,
        ms.current_location_id, l.zone_type, ms.alert_level, ms.readiness_score
),
fortress_security_evolution AS (
    SELECT 
        EXTRACT(YEAR FROM e.date) AS year,
        COUNT(DISTINCT ca.attack_id) AS total_attacks,
        SUM(CASE WHEN ca.outcome = 'Repelled' THEN 1 ELSE 0 END) / 
            NULLIF(COUNT(DISTINCT ca.attack_id), 0) AS defense_success_rate,
        COUNT(DISTINCT ds.structure_id) AS total_defenses,
        SUM(ds.quality * ds.effectiveness_rating) / 
            NULLIF(COUNT(DISTINCT ds.structure_id), 0) AS avg_defense_quality,
        AVG(zv.fortification_level) AS avg_fortification_level,
        COUNT(DISTINCT ms.squad_id) AS military_squads,
        SUM(ms.readiness_score * ms.active_members) / 
            NULLIF(SUM(ms.active_members), 0) AS weighted_military_readiness,
        SUM(e.resource_allocation) AS defense_investment,
        SUM(e.fortress_casualties) AS annual_casualties,
        -- Year over year improvement
        LAG(SUM(CASE WHEN ca.outcome = 'Repelled' THEN 1 ELSE 0 END) / 
            NULLIF(COUNT(DISTINCT ca.attack_id), 0)) 
            OVER (ORDER BY EXTRACT(YEAR FROM e.date)) AS previous_year_success_rate
    FROM 
        fortress_events e
    LEFT JOIN 
        creature_attacks ca ON EXTRACT(YEAR FROM ca.date) = EXTRACT(YEAR FROM e.date)
    LEFT JOIN 
        defense_structures ds ON EXTRACT(YEAR FROM ds.construction_date) <= EXTRACT(YEAR FROM e.date)
            AND (ds.decommission_date IS NULL OR EXTRACT(YEAR FROM ds.decommission_date) > EXTRACT(YEAR FROM e.date))
    LEFT JOIN 
        zone_vulnerability zv ON 1=1 -- Join all zones for averaging
    LEFT JOIN 
        military_readiness ms ON 1=1 -- Join all military units for counting
    WHERE 
        e.event_type = 'Security Assessment'
    GROUP BY 
        EXTRACT(YEAR FROM e.date)
    ORDER BY 
        EXTRACT(YEAR FROM e.date)
)
SELECT 
    -- Overall security metrics
    (SELECT COUNT(*) FROM creature_attacks) AS total_recorded_attacks,
    (SELECT COUNT(DISTINCT creature_id) FROM creature_attacks) AS unique_attackers,
    (SELECT SUM(fortress_casualties) FROM creature_attacks) AS total_historical_casualties,
    (SELECT 
        SUM(CASE WHEN outcome = 'Repelled' THEN 1 ELSE 0 END)::DECIMAL / 
        NULLIF(COUNT(*), 0) * 100 
     FROM creature_attacks) AS overall_defense_success_rate,
    
    -- Current threat assessment with REST API format
    JSON_OBJECT(
        'threat_assessment', (
            SELECT JSON_OBJECT(
                'current_threat_level', (
                    SELECT 
                        CASE 
                            WHEN COUNT(*) FILTER (WHERE date > CURRENT_DATE - INTERVAL '30 days') > 10 THEN 'Critical'
                            WHEN COUNT(*) FILTER (WHERE date > CURRENT_DATE - INTERVAL '30 days') > 5 THEN 'High'
                            WHEN COUNT(*) FILTER (WHERE date > CURRENT_DATE - INTERVAL '30 days') > 2 THEN 'Moderate'
                            ELSE 'Low'
                        END
                    FROM creature_attacks
                ),
                'active_threats', (
                    SELECT JSON_ARRAYAGG(
                        JSON_OBJECT(
                            'creature_type', c.type,
                            'threat_level', c.threat_level,
                            'last_sighting_date', cs.date,
                            'territory_proximity', ct.distance_to_fortress,
                            'estimated_numbers', c.estimated_population,
                            'creature_ids', (
                                SELECT JSON_ARRAYAGG(c2.creature_id)
                                FROM creatures c2
                                WHERE c2.type = c.type AND c2.active = TRUE
                            )
                        )
                    )
                    FROM creatures c
                    JOIN creature_sightings cs ON c.creature_id = cs.creature_id
                    JOIN creature_territories ct ON c.creature_id = ct.creature_id
                    WHERE 
                        c.active = TRUE AND
                        cs.date > CURRENT_DATE - INTERVAL '90 days'
                    GROUP BY c.type, c.threat_level, c.estimated_population
                ),
                'seasonal_risk_factors', (
                    SELECT JSON_OBJECT(
                        'current_season', EXTRACT(SEASON FROM CURRENT_DATE),
                        'historical_attack_frequency', COALESCE(
                            (SELECT AVG(attack_count)
                             FROM seasonal_attack_patterns 
                             WHERE season = EXTRACT(SEASON FROM CURRENT_DATE)),
                            0
                        ),
                        'expected_threat_level', COALESCE(
                            (SELECT AVG(avg_threat_level)
                             FROM seasonal_attack_patterns 
                             WHERE season = EXTRACT(SEASON FROM CURRENT_DATE)),
                            0
                        ),
                        'correlation_factors', (
                            SELECT JSON_ARRAYAGG(
                                JSON_OBJECT(
                                    'factor', f.factor_name,
                                    'correlation_strength', f.correlation_value
                                )
                            )
                            FROM (
                                SELECT 'Temperature' AS factor_name, 
                                       CORR(sap.attack_count, sap.temperature) AS correlation_value
                                FROM seasonal_attack_patterns sap
                                UNION ALL
                                SELECT 'Precipitation', 
                                       CORR(sap.attack_count, sap.precipitation)
                                FROM seasonal_attack_patterns sap
                                UNION ALL
                                SELECT 'Moon Phase', 
                                       CORR(sap.attack_count, 
                                            CASE 
                                                WHEN sap.moon_phase = 'Full' THEN 1.0
                                                WHEN sap.moon_phase = 'Waxing Gibbous' THEN 0.75
                                                WHEN sap.moon_phase = 'First Quarter' THEN 0.5
                                                WHEN sap.moon_phase = 'Waxing Crescent' THEN 0.25
                                                WHEN sap.moon_phase = 'New' THEN 0.0
                                                WHEN sap.moon_phase = 'Waning Crescent' THEN 0.25
                                                WHEN sap.moon_phase = 'Last Quarter' THEN 0.5
                                                WHEN sap.moon_phase = 'Waning Gibbous' THEN 0.75
                                                ELSE 0.5
                                            END)
                                FROM seasonal_attack_patterns sap
                            ) f
                            WHERE f.correlation_value IS NOT NULL
                        )
                    )
                )
            )
        ),
        'vulnerability_analysis', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'zone_id', zv.zone_id,
                    'zone_name', zv.zone_name,
                    'vulnerability_score', ROUND(
                        (zv.breaches::DECIMAL / NULLIF(zv.total_attacks, 0) * 0.4) +
                        ((5 - zv.fortification_level) * 0.2) +
                        (zv.access_points * 0.1) +
                        ((120 - COALESCE(zv.min_response_time, 120)) / 120 * 0.1) +
                        ((3 - COALESCE(zv.defense_structures, 0)) * 0.1) +
                        ((3 - COALESCE(zv.patrolling_squads, 0)) * 0.1)
                    , 2),
                    'historical_breaches', zv.breaches,
                    'fortification_level', zv.fortification_level,
                    'access_points', zv.access_points,
                    'military_response_time', zv.min_response_time,
                    'defense_coverage', (
                        SELECT JSON_OBJECT(
                            'structure_ids', (
                                SELECT JSON_ARRAYAGG(ds.structure_id)
                                FROM defense_structures ds
                                WHERE ds.location_id = zv.zone_id
                            ),
                            'squad_ids', (
                                SELECT JSON_ARRAYAGG(DISTINCT sm.squad_id)
                                FROM squad_movement sm
                                WHERE sm.patrol_zone_id = zv.zone_id
                            )
                        )
                    )
                )
            )
            FROM zone_vulnerability zv
            ORDER BY 
                (zv.breaches::DECIMAL / NULLIF(zv.total_attacks, 0) * 0.4) +
                ((5 - zv.fortification_level) * 0.2) +
                (zv.access_points * 0.1) +
                ((120 - COALESCE(zv.min_response_time, 120)) / 120 * 0.1) +
                ((3 - COALESCE(zv.defense_structures, 0)) * 0.1) +
                ((3 - COALESCE(zv.patrolling_squads, 0)) * 0.1) DESC
            LIMIT 10
        ),
        'defense_effectiveness', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'defense_type', x.defense_type,
                    'effectiveness_rate', x.effectiveness_rate,
                    'avg_enemy_casualties', x.avg_enemy_casualties,
                    'count', x.count,
                    'avg_quality', x.avg_quality,
                    'structure_ids', (
                        SELECT JSON_ARRAYAGG(ds.structure_id)
                        FROM defense_structures ds
                        WHERE ds.type = x.defense_type
                    )
                )
            )
            FROM (
                SELECT 
                    dse.defense_type,
                    ROUND(SUM(dse.successful_defenses)::DECIMAL / 
                        NULLIF(SUM(dse.attacks_defended), 0) * 100, 2) AS effectiveness_rate,
                    ROUND(AVG(dse.avg_enemy_casualties), 1) AS avg_enemy_casualties,
                    COUNT(*) AS count,
                    ROUND(AVG(dse.quality), 1) AS avg_quality
                FROM defense_structure_effectiveness dse
                GROUP BY dse.defense_type
                ORDER BY SUM(dse.successful_defenses)::DECIMAL / 
                        NULLIF(SUM(dse.attacks_defended), 0) DESC
            ) x
        ),
        'military_readiness_assessment', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'squad_id', mr.squad_id,
                    'squad_name', mr.squad_name,
                    'readiness_score', mr.readiness_score,
                    'active_members', mr.active_members,
                    'avg_combat_skill', mr.avg_combat_skill,
                    'avg_equipment_quality', mr.avg_equipment_quality,
                    'combat_effectiveness', ROUND(
                        (mr.successful_defenses::DECIMAL / NULLIF(mr.battles_participated, 0) * 0.4) +
                        (mr.avg_combat_skill / 10 * 0.3) +
                        (mr.avg_equipment_quality / 5 * 0.2) +
                        (CASE WHEN mr.days_since_training < 7 THEN 1.0
                              WHEN mr.days_since_training < 14 THEN 0.8
                              WHEN mr.days_since_training < 30 THEN 0.6
                              WHEN mr.days_since_training < 60 THEN 0.4
                              ELSE 0.2 END * 0.1)
                    , 2),
                    'station_location', (
                        SELECT JSON_OBJECT(
                            'zone_id', l.zone_id,
                            'zone_name', l.name,
                            'zone_type', l.zone_type
                        )
                        FROM locations l
                        WHERE l.location_id = mr.current_location_id
                    ),
                    'response_coverage', (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'zone_id', mcz.zone_id,
                                'response_time', mcz.response_time_minutes
                            )
                        )
                        FROM military_coverage_zones mcz
                        WHERE mcz.squad_id = mr.squad_id
                    )
                )
            )
            FROM military_readiness mr
            ORDER BY 
                (mr.successful_defenses::DECIMAL / NULLIF(mr.battles_participated, 0) * 0.4) +
                (mr.avg_combat_skill / 10 * 0.3) +
                (mr.avg_equipment_quality / 5 * 0.2) +
                (CASE WHEN mr.days_since_training < 7 THEN 1.0
                      WHEN mr.days_since_training < 14 THEN 0.8
                      WHEN mr.days_since_training < 30 THEN 0.6
                      WHEN mr.days_since_training < 60 THEN 0.4
                      ELSE 0.2 END * 0.1) DESC
        ),
        'security_evolution', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'year', fse.year,
                    'defense_success_rate', ROUND(fse.defense_success_rate * 100, 2),
                    'total_attacks', fse.total_attacks,
                    'avg_defense_quality', fse.avg_defense_quality,
                    'military_readiness', fse.weighted_military_readiness,
                    'casualties', fse.annual_casualties,
                    'year_over_year_improvement', ROUND(
                        CASE 
                            WHEN fse.previous_year_success_rate IS NULL THEN NULL
                            ELSE (fse.defense_success_rate - fse.previous_year_success_rate) * 100
                        END
                    , 2),
                    'defense_investment', fse.defense_investment
                )
            )
            FROM fortress_security_evolution fse
        ),
        'recommendation_summary', (
            SELECT JSON_ARRAYAGG(
                JSON_OBJECT(
                    'recommendation_type', 
                    CASE r.priority
                        WHEN 1 THEN 'Critical'
                        WHEN 2 THEN 'High'
                        WHEN 3 THEN 'Medium'
                        ELSE 'Low'
                    END,
                    'focus_area', r.focus_area,
                    'recommendation', r.recommendation,
                    'estimated_resources', r.estimated_resources,
                    'expected_improvement', ROUND(r.expected_improvement * 100, 2),
                    'related_entities', (
                        CASE r.focus_area
                            WHEN 'Zone Security' THEN (
                                SELECT JSON_OBJECT('zone_ids', JSON_ARRAYAGG(zv.zone_id))
                                FROM zone_vulnerability zv
                                WHERE zv.vulnerability_score > 0.5
                                LIMIT 5
                            )
                            WHEN 'Military Readiness' THEN (
                                SELECT JSON_OBJECT('squad_ids', JSON_ARRAYAGG(mr.squad_id))
                                FROM military_readiness mr
                                WHERE mr.readiness_score < 0.7
                                LIMIT 5
                            )
                            WHEN 'Defense Structures' THEN (
                                SELECT JSON_OBJECT('structure_ids', JSON_ARRAYAGG(ds.structure_id))
                                FROM defense_structures ds
                                WHERE ds.days_since_maintenance > 90 OR ds.quality < 3
                                LIMIT 5
                            )
                            ELSE NULL
                        END
                    )
                )
            )
            FROM security_recommendations r
            ORDER BY r.priority, r.expected_improvement DESC
        )
    ) AS security_analysis
FROM (SELECT 1) AS dummy;