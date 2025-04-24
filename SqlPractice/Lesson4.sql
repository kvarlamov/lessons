/*Задача 2: Получение данных о гноме с навыками и назначениями

Создайте запрос, который возвращает информацию о гноме, включая идентификаторы всех его навыков, текущих назначений, принадлежности к отрядам и используемого снаряжения.

Что примерно выдаст REST на основании этих данных:

[
  {
    "dwarf_id": 101,
    "name": "Urist McMiner",
    "age": 65,
    "profession": "Miner",
    "related_entities": {
      "skill_ids": [1001, 1002, 1003],
      "assignment_ids": [2001, 2002],
      "squad_ids": [401],
      "equipment_ids": [5001, 5002, 5003]
    }
  }
]*/
SELECT 
    d.dwarf_id,
    d.name,
    d.age,
    d.profession,
    JSON_OBJECT(
        'skill_ids', (
            SELECT JSON_ARRAYAGG(ds.skill_id)
            FROM dwarf_skills ds
            WHERE ds.dwarf_id = d.dwarf_id
        ),
        'assignment_ids', (
            SELECT JSON_ARRAYAGG(da.assignment_id)
            FROM dwarf_assignments da
            WHERE da.dwarf_id = d.dwarf_id AND da.end_date IS NULL
        ),
        'squad_ids', (
            SELECT JSON_ARRAYAGG(sm.squad_id)
            FROM squad_members sm
            WHERE sm.dwarf_id = d.dwarf_id AND sm.exit_date IS NULL
        ),
        'equipment_ids', (
            SELECT JSON_ARRAYAGG(de.equipment_id)
            FROM dwarf_equipment de
            WHERE de.dwarf_id = d.dwarf_id
        )
    ) AS related_entities
FROM 
    dwarves d;


/*Задача 3: Данные о мастерской с назначенными рабочими и проектами

Напишите запрос для получения информации о мастерской, включая идентификаторы назначенных ремесленников, текущих проектов, используемых и производимых ресурсов.

Что примерно выдаст REST на основании этих данных:

[
  {
    "workshop_id": 301,
    "name": "Royal Forge",
    "type": "Smithy",
    "quality": "Masterwork",
    "related_entities": {
      "craftsdwarf_ids": [101, 103],
      "project_ids": [701, 702, 703],
      "input_material_ids": [201, 204],
      "output_product_ids": [801, 802]
    }
  }
]*/
SELECT 
    w.workshop_id,
    w.name,
    w.type,
    w.quality,
    JSON_OBJECT(
        'craftsdwarf_ids', (
            SELECT JSON_ARRAYAGG(cw.dwarf_id)
            FROM workshop_craftsdwarves cw
            WHERE cw.workshop_id = w.workshop_id
        ),
        'project_ids', (
            SELECT JSON_ARRAYAGG(p.project_id)
            FROM projects p
            WHERE p.workshop_id = w.workshop_id AND p.status = 'active'
        ),
        'input_material_ids', (
            SELECT JSON_ARRAYAGG(wm.material_id)
            FROM workshop_materials wm
            WHERE wm.workshop_id = w.workshop_id AND wm.is_input = TRUE
        ),
        'output_product_ids', (
            SELECT JSON_ARRAYAGG(wp.product_id)
            FROM workshop_products wp
            WHERE wp.workshop_id = w.workshop_id
        )
    ) AS related_entities
FROM 
    workshops w;

/*Задача 4: Данные о военном отряде с составом и операциями

Разработайте запрос, который возвращает информацию о военном отряде, включая идентификаторы всех членов отряда, используемого снаряжения, прошлых и текущих операций, тренировок.

Что примерно выдаст REST на основании этих данных:

[
  {
    "squad_id": 401,
    "name": "The Axe Lords",
    "formation_type": "Melee",
    "leader_id": 102,
    "related_entities": {
      "member_ids": [102, 104, 105, 107, 110],
      "equipment_ids": [5004, 5005, 5006, 5007, 5008],
      "operation_ids": [601, 602],
      "training_schedule_ids": [901, 902],
      "battle_report_ids": [1101, 1102, 1103]
    }
  }
]*/
SELECT 
    ms.squad_id,
    ms.name,
    ms.formation_type,
    ms.leader_id,
    JSON_OBJECT(
        'member_ids', (
            SELECT JSON_ARRAYAGG(sm.dwarf_id)
            FROM squad_members sm
            WHERE sm.squad_id = ms.squad_id AND sm.exit_date IS NULL
        ),
        'equipment_ids', (
            SELECT JSON_ARRAYAGG(se.equipment_id)
            FROM squad_equipment se
            WHERE se.squad_id = ms.squad_id
        ),
        'operation_ids', (
            SELECT JSON_ARRAYAGG(so.operation_id)
            FROM squad_operations so
            WHERE so.squad_id = ms.squad_id AND so.status IN ('completed', 'ongoing')
        ),
        'training_schedule_ids', (
            SELECT JSON_ARRAYAGG(st.schedule_id)
            FROM squad_training st
            WHERE st.squad_id = ms.squad_id
        ),
        'battle_report_ids', (
            SELECT JSON_ARRAYAGG(sb.report_id)
            FROM squad_battles sb
            WHERE sb.squad_id = ms.squad_id
        )
    ) AS related_entities
FROM 
    military_squads ms;