/*
Common Table Expression (CTE) --- удобный способ хранить временные результаты для дальнейших операций с ними. Представляет собой альтернативу для составных запросов, упрощая читаемость кода. Также улучшается производительность, за счёт устранения избыточных операций и сложных вложенных запросов, посредством сохранения промежуточных результатов вычислений. Объявляется временная таблица
WITH <имя таблицы> AS
к которой возможно обращение из основного запроса также, как и к другим таблицам в базе. В решении используется дважды для удобства вычисления результатов экспедиции и улучшений навыков.

COALESCE принимает список выражений, возвращая первое значение выражения, которое вычислено как не-NULL. В решении неоднократно используется для задания "особого случая" (например, в случае, если ценность найденных значений NULL, значит не найдено ни одного артефакта, т.е. общая ценность 0).

NULLIF принимает два выражения, возвращая NULL в случае равенства выражений, иначе возвращает первое выражение. В решении используется например для установки в NULL поля "encounter_success_rate" - для случая, когда за экспедицию не было не одной встречи (поле примет значение NULL).

QUARTER извлекает квартал из даты, хранящейся в строке.

LAG позволяет получить значение из предыдущей строки (или ранее) в наборе относительно текущей строки. Она часто используется для сравнения текущего значения с предыдущим, что полезно для анализа изменений или вычисления разниц между последовательными записями.


Задача 2*: Комплексный анализ эффективности производства

Разработайте запрос, который анализирует эффективность каждой мастерской, учитывая:
- Производительность каждого ремесленника (соотношение созданных продуктов к затраченному времени)
- Эффективность использования ресурсов (соотношение потребляемых ресурсов к производимым товарам)
- Качество производимых товаров (средневзвешенное по ценности)
- Время простоя мастерской
- Влияние навыков ремесленников на качество товаров

Возможный вариант выдачи:


[
  {
    "workshop_id": 301,
    "workshop_name": "Royal Forge",
    "workshop_type": "Smithy",
    "num_craftsdwarves": 4,
    "total_quantity_produced": 256,
    "total_production_value": 187500,
    
    "daily_production_rate": 3.41,
    "value_per_material_unit": 7.82,
    "workshop_utilization_percent": 85.33,
    
    "material_conversion_ratio": 1.56,
    
    "average_craftsdwarf_skill": 7.25,
    
    "skill_quality_correlation": 0.83,
    
    "related_entities": {
      "craftsdwarf_ids": [101, 103, 108, 115],
      "product_ids": [801, 802, 803, 804, 805, 806],
      "material_ids": [201, 204, 208, 210],
      "project_ids": [701, 702, 703]
    }
  },
  {
    "workshop_id": 304,
    "workshop_name": "Gemcutter's Studio",
    "workshop_type": "Jewelcrafting",
    "num_craftsdwarves": 2,
    "total_quantity_produced": 128,
    "total_production_value": 205000,
    
    "daily_production_rate": 2.56,
    "value_per_material_unit": 13.67,
    "workshop_utilization_percent": 78.95,
    
    "material_conversion_ratio": 0.85,
    
    "average_craftsdwarf_skill": 8.50,
    
    "skill_quality_correlation": 0.92,
    
    "related_entities": {
      "craftsdwarf_ids": [105, 112],
      "product_ids": [820, 821, 822, 823, 824],
      "material_ids": [206, 213, 217, 220],
      "project_ids": [705, 708]
    }
  }
]*/

WITH workshop_data AS (
    SELECT 
        w.workshop_id,
        w.name AS workshop_name,
        w.type AS workshop_type,
        COUNT(DISTINCT wc.dwarf_id) AS num_craftsdwarves,
        SUM(wp.quantity) AS total_quantity_produced,
        SUM(p.value * wp.quantity) AS total_production_value,
        AVG(wp.quantity / NULLIF(DATEDIFF(wp.production_date, pm.start_date), 0)) AS daily_production_rate,
        SUM(wp.quantity * p.value) / NULLIF(SUM(wm.quantity), 0) AS value_per_material_unit,
        SUM(wm.quantity) / NULLIF(SUM(wp.quantity), 0) AS material_conversion_ratio,
        (SUM(DATEDIFF(pm.end_date, pm.start_date)) / DATEDIFF(MAX(pm.end_date), MIN(pm.start_date))) * 100 AS workshop_utilization_percent,
        AVG(ds.level) AS average_craftsdwarf_skill
    FROM 
        workshops w
    LEFT JOIN
        workshop_craftsdwarves wc ON w.workshop_id = wc.workshop_id
    LEFT JOIN
        workshop_products wp ON w.workshop_id = wp.workshop_id
    LEFT JOIN
        products p ON wp.product_id = p.product_id
    LEFT JOIN
        project_members pm ON wp.workshop_id = pm.workshop_id
    LEFT JOIN 
        dwarf_skills ds ON wc.dwarf_id = ds.dwarf_id
    LEFT JOIN 
        workshop_materials wm ON wm.workshop_id = w.workshop_id
    GROUP BY 
        w.workshop_id, w.name, w.type
)
SELECT 
    wd.workshop_id,
    wd.workshop_name,
    wd.workshop_type,
    wd.num_craftsdwarves,
    wd.total_quantity_produced,
    wd.total_production_value,
    wd.daily_production_rate,
    wd.value_per_material_unit,
    wd.workshop_utilization_percent,
    wd.material_conversion_ratio,
    wd.average_craftsdwarf_skill,
    JSON_OBJECT(
        'craftsdwarf_ids', (
            SELECT JSON_ARRAYAGG(DISTINCT wc.dwarf_id)
            FROM workshop_craftsdwarves wc
            WHERE wc.workshop_id = wd.workshop_id
        ),
        'product_ids', (
            SELECT JSON_ARRAYAGG(DISTINCT wp.product_id)
            FROM workshop_products wp
            WHERE wp.workshop_id = wd.workshop_id
        ),
        'material_ids', (
            SELECT JSON_ARRAYAGG(DISTINCT wm.material_id)
            FROM workshop_materials wm
            WHERE wm.workshop_id = wd.workshop_id
        ),
        'project_ids', (
            SELECT JSON_ARRAYAGG(DISTINCT pm.project_id)
            FROM project_members pm
            WHERE pm.workshop_id = wd.workshop_id
        )
    ) AS related_entities
FROM 
    workshop_data wd;