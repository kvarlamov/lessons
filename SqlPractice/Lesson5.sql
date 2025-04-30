/*Задача 1*: Анализ эффективности экспедиций

Напишите запрос, который определит наиболее и наименее успешные экспедиции, учитывая:
- Соотношение выживших участников к общему числу
- Ценность найденных артефактов
- Количество обнаруженных новых мест
- Успешность встреч с существами (отношение благоприятных исходов к неблагоприятным)
- Опыт, полученный участниками (сравнение навыков до и после)

Возможный вариант выдачи:


[
  {
    "expedition_id": 2301,
    "destination": "Ancient Ruins",
    "status": "Completed",
    "survival_rate": 71.43,
    "artifacts_value": 28500,
    "discovered_sites": 3,
    "encounter_success_rate": 66.67,
    "skill_improvement": 14,
    "expedition_duration": 44,
    "overall_success_score": 0.78,
    "related_entities": {
      "member_ids": [102, 104, 107, 110, 112, 115, 118],
      "artifact_ids": [2501, 2502, 2503],
      "site_ids": [2401, 2402, 2403]
    }
  },
  {
    "expedition_id": 2305,
    "destination": "Deep Caverns",
    "status": "Completed",
    "survival_rate": 80.00,
    "artifacts_value": 42000,
    "discovered_sites": 2,
    "encounter_success_rate": 83.33,
    "skill_improvement": 18,
    "expedition_duration": 38,
    "overall_success_score": 0.85,
    "related_entities": {
      "member_ids": [103, 105, 108, 113, 121],
      "artifact_ids": [2508, 2509, 2510, 2511],
      "site_ids": [2410, 2411]
    }
  },
  {
    "expedition_id": 2309,
    "destination": "Abandoned Fortress",
    "status": "Completed",
    "survival_rate": 50.00,
    "artifacts_value": 56000,
    "discovered_sites": 4,
    "encounter_success_rate": 42.86,
    "skill_improvement": 23,
    "expedition_duration": 62,
    "overall_success_score": 0.63,
    "related_entities": {
      "member_ids": [106, 109, 111, 119, 124, 125],
      "artifact_ids": [2515, 2516, 2517, 2518, 2519],
      "site_ids": [2420, 2421, 2422, 2423]
    }
  }
]*/


SELECT 
    e.expedition_id,
    e.destination,
    e.status,
    (
        SELECT 
            (COUNT(CASE WHEN em.survived THEN 1 ELSE NULL END) / COUNT(*)) * 100
        FROM 
            expedition_members em
        WHERE 
            em.expedition_id = e.expedition_id
    ) AS survival_rate,
    (
        SELECT 
            COALESCE(SUM(a.value), 0)
        FROM 
            expedition_artifacts ea
        JOIN 
            artifacts a ON ea.artifact_id = a.artifact_id
        WHERE 
            ea.expedition_id = e.expedition_id
    ) AS artifacts_value,
    (
        SELECT 
            COUNT(DISTINCT es.site_id)
        FROM 
            expedition_sites es
        WHERE 
            es.expedition_id = e.expedition_id
    ) AS discovered_sites,
    (
        SELECT
            (COUNT(CASE WHEN ec.outcome = TRUE THEN 1 ELSE NULL END) / NULLIF(COUNT(*), 0)) * 100
        FROM
            expedition_creatures ec
        WHERE
            ec.expedition_id = e.expedition_id
    ) AS encounter_success_rate,
    (
        SELECT 
            COALESCE(SUM(dafter.skill_level) - SUM(dbefore.skill_level), 0)
        FROM 
            expedition_members em
        JOIN 
            dwarf_skills dbefore ON em.dwarf_id = dbefore.dwarf_id AND dbefore.date < e.departure_date
        JOIN 
            dwarf_skills dafter ON em.dwarf_id = dafter.dwarf_id AND dafter.date > e.return_date
        WHERE 
            em.expedition_id = e.expedition_id
    ) AS skill_improvement,
    DATEDIFF(e.return_date, e.departure_date) AS expedition_duration,
    JSON_OBJECT(
        'member_ids', (SELECT JSON_ARRAYAGG(DISTINCT em.dwarf_id) FROM expedition_members em WHERE em.expedition_id = e.expedition_id),
        'artifact_ids', (SELECT JSON_ARRAYAGG(DISTINCT ea.artifact_id) FROM expedition_artifacts ea WHERE ea.expedition_id = e.expedition_id),
        'site_ids', (SELECT JSON_ARRAYAGG(DISTINCT es.site_id) FROM expedition_sites es WHERE es.expedition_id = e.expedition_id)
    ) AS related_entities
FROM 
    expeditions e;