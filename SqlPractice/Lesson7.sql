/*
Комплексная оценка военной эффективности отрядов

Создайте запрос, оценивающий эффективность военных отрядов на основе:
- Результатов всех сражений (победы/поражения/потери)
- Соотношения побед к общему числу сражений
- Навыков членов отряда и их прогресса
- Качества экипировки
- Истории тренировок и их влияния на результаты
- Выживаемости членов отряда в долгосрочной перспективе

Возможный вариант выдачи:


[
  {
    "squad_id": 401,
    "squad_name": "The Axe Lords",
    "formation_type": "Melee",
    "leader_name": "Urist McAxelord",
    "total_battles": 28,
    "victories": 22,
    "victory_percentage": 78.57,
    "casualty_rate": 24.32,
    "casualty_exchange_ratio": 3.75,
    "current_members": 8,
    "total_members_ever": 12,
    "retention_rate": 66.67,
    "avg_equipment_quality": 4.28,
    "total_training_sessions": 156,
    "avg_training_effectiveness": 0.82,
    "training_battle_correlation": 0.76,
    "avg_combat_skill_improvement": 3.85,
    "overall_effectiveness_score": 0.815,
    "related_entities": {
      "member_ids": [102, 104, 105, 107, 110, 115, 118, 122],
      "equipment_ids": [5001, 5002, 5003, 5004, 5005, 5006, 5007, 5008, 5009],
      "battle_report_ids": [1101, 1102, 1103, 1104, 1105, 1106, 1107, 1108],
      "training_ids": [901, 902, 903, 904, 905, 906]
    }
  },
  {
    "squad_id": 403,
    "squad_name": "Crossbow Legends",
    "formation_type": "Ranged",
    "leader_name": "Dokath Targetmaster",
    "total_battles": 22,
    "victories": 18,
    "victory_percentage": 81.82,
    "casualty_rate": 16.67,
    "casualty_exchange_ratio": 5.20,
    "current_members": 7,
    "total_members_ever": 10,
    "retention_rate": 70.00,
    "avg_equipment_quality": 4.45,
    "total_training_sessions": 132,
    "avg_training_effectiveness": 0.88,
    "training_battle_correlation": 0.82,
    "avg_combat_skill_improvement": 4.12,
    "overall_effectiveness_score": 0.848,
    "related_entities": {
      "member_ids": [106, 109, 114, 116, 119, 123, 125],
      "equipment_ids": [5020, 5021, 5022, 5023, 5024, 5025, 5026],
      "battle_report_ids": [1120, 1121, 1122, 1123, 1124, 1125],
      "training_ids": [920, 921, 922, 923, 924]
    }
  }
]
*/

WITH BattleStats AS (
  SELECT 
    sb.squad_id,
    COUNT(sb.report_id) AS total_battles,
    SUM(CASE WHEN sb.outcome = 'victory' THEN 1 ELSE 0 END) AS victories,
    SUM(sb.casualties) AS total_casualties,
    SUM(sb.enemy_casualties) AS total_enemy_casualties
  FROM SQUAD_BATTLES sb
  GROUP BY sb.squad_id
),
SquadMembersStats AS (
  SELECT 
    sm.squad_id,
    COUNT(*) AS total_members_ever,
    COUNT(CASE WHEN sm.exit_date IS NULL THEN 1 END) AS current_members
  FROM SQUAD_MEMBERS sm
  GROUP BY sm.squad_id
),
EquipmentStats AS (
  SELECT 
    se.squad_id,
    AVG(e.quality) AS avg_equipment_quality
  FROM SQUAD_EQUIPMENT se
  JOIN EQUIPMENT e ON se.equipment_id = e.equipment_id
  GROUP BY se.squad_id
),
TrainingStats AS (
  SELECT 
    st.squad_id,
    COUNT(*) AS total_training_sessions,
    AVG(st.effectiveness) AS avg_training_effectiveness
  FROM SQUAD_TRAINING st
  GROUP BY st.squad_id
)
SELECT ms.squad_id,
       ms.name AS squad_name,
       ms.formation_type,
       (SELECT d.name FROM DWARVES d WHERE d.dwarf_id = ms.leader_id) AS leader_name,
       bs.total_battles,
       bs.victories,
       ROUND(100.0 * bs.victories / NULLIF(bs.total_battles, 0), 2) AS victory_percentage,
       ROUND(100.0 * bs.total_casualties / NULLIF((bs.total_casualties + bs.total_enemy_casualties), 0), 2) AS casualty_rate,
       ROUND(bs.total_enemy_casualties / NULLIF(bs.total_casualties, 0), 2) AS casualty_exchange_ratio,
       sms.current_members,
       sms.total_members_ever,
       ROUND(100.0 * sms.current_members / NULLIF(sms.total_members_ever, 0), 2) AS retention_rate,
       es.avg_equipment_quality,
       ts.total_training_sessions,
       ts.avg_training_effectiveness,
       JSON_OBJECT(
         'member_ids', (SELECT JSON_ARRAYAGG(sm.dwarf_id) FROM SQUAD_MEMBERS sm WHERE sm.squad_id = ms.squad_id AND sm.exit_date IS NULL),
         'equipment_ids', (SELECT JSON_ARRAYAGG(se.equipment_id) FROM SQUAD_EQUIPMENT se WHERE se.squad_id = ms.squad_id),
         'battle_report_ids', (SELECT JSON_ARRAYAGG(sb.report_id) FROM SQUAD_BATTLES sb WHERE sb.squad_id = ms.squad_id),
         'training_ids', (SELECT JSON_ARRAYAGG(st.schedule_id) FROM SQUAD_TRAINING st WHERE st.squad_id = ms.squad_id)
       ) AS related_entities
FROM MILITARY_SQUADS ms
LEFT JOIN BattleStats bs ON ms.squad_id = bs.squad_id
LEFT JOIN SquadMembersStats sms ON ms.squad_id = sms.squad_id
LEFT JOIN EquipmentStats es ON ms.squad_id = es.squad_id
LEFT JOIN TrainingStats ts ON ms.squad_id = ts.squad_id;


/*поля
"training_battle_correlation",
    "avg_combat_skill_improvement",
    "overall_effectiveness_score"
  не выводил  
    */