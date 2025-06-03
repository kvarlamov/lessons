/*
Задача 5*: Многофакторный анализ угроз и безопасности крепости

Разработайте запрос, который комплексно анализирует безопасность крепости, учитывая:
- Историю всех атак существ и их исходов
- Эффективность защитных сооружений
- Соотношение между типами существ и результативностью обороны
- Оценку уязвимых зон на основе архитектуры крепости
- Корреляцию между сезонными факторами и частотой нападений
- Готовность военных отрядов и их расположение
- Эволюцию защитных способностей крепости со временем

Возможный вариант выдачи:


{
  "total_recorded_attacks": 183,
  "unique_attackers": 42,
  "overall_defense_success_rate": 76.50,
  "security_analysis": {
    "threat_assessment": {
      "current_threat_level": "Moderate",
      "active_threats": [
        {
          "creature_type": "Goblin",
          "threat_level": 3,
          "last_sighting_date": "0205-08-12",
          "territory_proximity": 1.2,
          "estimated_numbers": 35,
          "creature_ids": [124, 126, 128, 132, 136]
        },
        {
          "creature_type": "Forgotten Beast",
          "threat_level": 5,
          "last_sighting_date": "0205-07-28",
          "territory_proximity": 3.5,
          "estimated_numbers": 1,
          "creature_ids": [158]
        }
      ]
    },
    "vulnerability_analysis": [
      {
        "zone_id": 15,
        "zone_name": "Eastern Gate",
        "vulnerability_score": 0.68,
        "historical_breaches": 8,
        "fortification_level": 2,
        "military_response_time": 48,
        "defense_coverage": {
          "structure_ids": [182, 183, 184],
          "squad_ids": [401, 405]
        }
      }
    ],
    "defense_effectiveness": [
      {
        "defense_type": "Drawbridge",
        "effectiveness_rate": 95.12,
        "avg_enemy_casualties": 12.4,
        "structure_ids": [185, 186, 187, 188]
      },
      {
        "defense_type": "Trap Corridor",
        "effectiveness_rate": 88.75,
        "avg_enemy_casualties": 8.2,
        "structure_ids": [201, 202, 203, 204]
      }
    ],
    "military_readiness_assessment": [
      {
        "squad_id": 403,
        "squad_name": "Crossbow Legends",
        "readiness_score": 0.92,
        "active_members": 7,
        "avg_combat_skill": 8.6,
        "combat_effectiveness": 0.85,
        "response_coverage": [
          {
            "zone_id": 12,
            "response_time": 0
          },
          {
            "zone_id": 15,
            "response_time": 36
          }
        ]
      }
    ],
    "security_evolution": [
      {
        "year": 203,
        "defense_success_rate": 68.42,
        "total_attacks": 38,
        "casualties": 42,
        "year_over_year_improvement": 3.20
      },
      {
        "year": 204,
        "defense_success_rate": 72.50,
        "total_attacks": 40,
        "casualties": 36,
        "year_over_year_improvement": 4.08
      }
    ]
  }
}
*/

WITH BasicStats AS (
    SELECT 
        COUNT(*) AS total_recorded_attacks,
        COUNT(DISTINCT creature_id) AS unique_attackers,
        100.0 * SUM(CASE WHEN outcome = 'Defended' THEN 1 ELSE 0 END) / COUNT(*) AS overall_defense_success_rate
    FROM CREATURE_ATTACKS
),
MaxDate AS (
    SELECT MAX(date) AS max_sighting_date FROM CREATURE_SIGHTINGS
),
ActiveThreats AS (
    SELECT DISTINCT c.creature_id
    FROM CREATURES c
    LEFT JOIN CREATURE_SIGHTINGS cs ON c.creature_id = cs.creature_id
    LEFT JOIN CREATURE_TERRITORIES ct ON c.creature_id = ct.creature_id
    WHERE (cs.date > (SELECT max_sighting_date FROM MaxDate) - INTERVAL '30 days') OR (ct.distance_to_fortress < 5)
),
ThreatByType AS (
    SELECT 
        c.type AS creature_type,
        MAX(c.threat_level) AS threat_level,
        MAX(cs.date) AS last_sighting_date,
        MIN(ct.distance_to_fortress) AS territory_proximity,
        SUM(c.estimated_population) AS estimated_numbers,
        ARRAY_AGG(DISTINCT c.creature_id) AS creature_ids
    FROM CREATURES c
    JOIN ActiveThreats at ON c.creature_id = at.creature_id
    LEFT JOIN CREATURE_SIGHTINGS cs ON c.creature_id = cs.creature_id
    LEFT JOIN CREATURE_TERRITORIES ct ON c.creature_id = ct.creature_id
    GROUP BY c.type
),
FortressThreatLevel AS (
    SELECT 
        CASE 
            WHEN MAX(threat_level) >= 5 THEN 'High'
            WHEN MAX(threat_level) >= 3 THEN 'Moderate'
            ELSE 'Low'
        END AS current_threat_level 
    FROM ThreatByType
),
LocationVulnerabilities AS (
    SELECT 
        l.location_id AS zone_id,
        l.name AS zone_name,
        COUNT(CASE WHEN ca.outcome = 'Breached' THEN 1 END) AS historical_breaches,
        l.fortification_level,
        MIN(mcz.response_time) AS military_response_time,
        ARRAY_AGG(DISTINCT mcz.squad_id) FILTER (WHERE mcz.squad_id IS NOT NULL) AS squad_ids
    FROM LOCATIONS l
    LEFT JOIN CREATURE_ATTACKS ca ON l.location_id = ca.location_id
    LEFT JOIN MILITARY_COVERAGE_ZONES mcz ON l.location_id = mcz.zone_id
    GROUP BY l.location_id, l.name, l.fortification_level
),
SquadReadiness AS (
    WITH ActiveSquadMembers AS (
        SELECT sm.squad_id, sm.dwarf_id
        FROM SQUAD_MEMBERS sm
        WHERE sm.exit_date IS NULL
    ),
    SquadCombatSkills AS (
        SELECT asm.squad_id, AVG(ds.level) AS avg_combat_skill
        FROM ActiveSquadMembers asm
        JOIN DWARF_SKILLS ds ON asm.dwarf_id = ds.dwarf_id
        JOIN SKILLS sk ON ds.skill_id = sk.skill_id AND sk.name = 'Combat'
        GROUP BY asm.squad_id
    ),
    SquadLatestTraining AS (
        SELECT st.squad_id, MAX(st.effectiveness) AS combat_effectiveness
        FROM SQUAD_TRAINING st
        GROUP BY st.squad_id
    ),
    SquadResponseCoverage AS (
        SELECT mcz.squad_id, 
               ARRAY_AGG(
                   JSON_OBJECT(
                       'zone_id', mcz.zone_id, 
                       'response_time', mcz.response_time
                   )
               ) AS response_coverage
        FROM MILITARY_COVERAGE_ZONES mcz
        GROUP BY mcz.squad_id
    )
    SELECT 
        s.squad_id,
        s.name AS squad_name,
        COUNT(asm.dwarf_id) AS active_members,
        COALESCE(scs.avg_combat_skill, 0) AS avg_combat_skill,
        COALESCE(slt.combat_effectiveness, 0) AS combat_effectiveness,
        COALESCE(src.response_coverage, '[]') AS response_coverage
    FROM MILITARY_SQUADS s
    LEFT JOIN ActiveSquadMembers asm ON s.squad_id = asm.squad_id
    LEFT JOIN SquadCombatSkills scs ON s.squad_id = scs.squad_id
    LEFT JOIN SquadLatestTraining slt ON s.squad_id = slt.squad_id
    LEFT JOIN SquadResponseCoverage src ON s.squad_id = src.squad_id
    GROUP BY s.squad_id, s.name, scs.avg_combat_skill, slt.combat_effectiveness, src.response_coverage
),
YearlyStats AS (
    SELECT 
        EXTRACT(YEAR FROM ca.date) AS year,
        SUM(CASE WHEN ca.outcome = 'Defended' THEN 1 ELSE 0 END) AS defended,
        COUNT(*) AS total_attacks,
        SUM(ca.casualties) AS casualties
    FROM CREATURE_ATTACKS ca
    GROUP BY EXTRACT(YEAR FROM ca.date)
),
SecurityEvolution AS (
    SELECT 
        year,
        100.0 * defended / NULLIF(total_attacks, 0) AS defense_success_rate,
        total_attacks,
        casualties,
        100.0 * defended / NULLIF(total_attacks, 0) - 
        LAG(100.0 * defended / NULLIF(total_attacks, 0)) OVER (ORDER BY year) AS year_over_year_improvement
    FROM YearlyStats
    ORDER BY year
)
SELECT 
    JSON_OBJECT(
        'total_recorded_attacks', bs.total_recorded_attacks,
        'unique_attackers', bs.unique_attackers,
        'overall_defense_success_rate', ROUND(bs.overall_defense_success_rate, 2),
        'security_analysis', JSON_OBJECT(
            'threat_assessment', JSON_OBJECT(
                'current_threat_level', ftl.current_threat_level,
                'active_threats', (
                    SELECT JSON_AGG(
                        JSON_OBJECT(
                            'creature_type', creature_type,
                            'threat_level', threat_level,
                            'last_sighting_date', last_sighting_date,
                            'territory_proximity', territory_proximity,
                            'estimated_numbers', estimated_numbers,
                            'creature_ids', creature_ids
                        )
                    ) FROM ThreatByType
                )
            ),
            'vulnerability_analysis', (
                SELECT JSON_AGG(
                    JSON_OBJECT(
                        'zone_id', zone_id,
                        'zone_name', zone_name,
                        'historical_breaches', historical_breaches,
                        'fortification_level', fortification_level,
                        'military_response_time', military_response_time,
                        'defense_coverage', JSON_OBJECT(
                            'squad_ids', squad_ids
                        )
                    )
                ) FROM LocationVulnerabilities
            ),
            'military_readiness_assessment', (
                SELECT JSON_AGG(
                    JSON_OBJECT(
                        'squad_id', squad_id,
                        'squad_name', squad_name,
                        'active_members', active_members,
                        'avg_combat_skill', ROUND(avg_combat_skill, 1),
                        'combat_effectiveness', ROUND(combat_effectiveness, 2),
                        'response_coverage', response_coverage
                    )
                ) FROM SquadReadiness
            ),
            'security_evolution', (
                SELECT JSON_AGG(
                    JSON_OBJECT(
                        'year', year,
                        'defense_success_rate', ROUND(defense_success_rate, 2),
                        'total_attacks', total_attacks,
                        'casualties', casualties,
                        'year_over_year_improvement', ROUND(year_over_year_improvement, 2)
                    )
                ) FROM SecurityEvolution
            )
        )
    ) AS security_analysis_report
FROM BasicStats bs
CROSS JOIN FortressThreatLevel ftl