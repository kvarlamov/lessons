/*База данных претерпела очередные кардинальные изменения :)

+---------------------+        +---------------------+        +---------------------+
| FORTRESSES          |        | DWARVES             |        | DWARF_SKILLS        |
+---------------------+        +---------------------+        +---------------------+
| fortress_id (PK)    |<---+   | dwarf_id (PK)       |<---+   | dwarf_id (FK)       |
| name                |    |   | name                |    |   | skill_id (FK)       |
| location            |    |   | age                 |    |   | level               |
| founded_year        |    |   | profession          |    |   | experience          |
| depth               |    |   | fortress_id (FK)    |    |   | date                |
| population          |    |   +---------------------+    |   +---------------------+
+---------------------+    |                              |
                           |                              |   +---------------------+
                           |                              |   | SKILLS              |
                           |                              |   +---------------------+
+---------------------+    |   +---------------------+    |   | skill_id (PK)       |
| WORKSHOPS           |    |   | DWARF_ASSIGNMENTS   |    |   | name                |
+---------------------+    |   +---------------------+    |   | category            |
| workshop_id (PK)    |<---+   | assignment_id (PK)  |    |   | description         |
| name                |    |   | dwarf_id (FK)       |----+   | skill_type          |
| type                |    |   | assignment_type     |        +---------------------+
| quality             |    |   | start_date          |
| fortress_id (FK)    |----+   | end_date            |        +---------------------+
+---------------------+        +---------------------+        | DWARF_EQUIPMENT     |
                                                              | dwarf_id (FK)       |
+---------------------+        +---------------------+        | equipment_id (FK)   |
| WORKSHOP_CRAFTSDWARVES|      | MILITARY_SQUADS     |        | quality             |
+---------------------+        +---------------------+        | equipped_date       |
| workshop_id (FK)    |        | squad_id (PK)       |<---+   +---------------------+
| dwarf_id (FK)       |        | name                |    |
| assignment_date     |        | formation_type      |    |   +---------------------+
| role                |        | leader_id (FK)      |----+   | EQUIPMENT           |
+---------------------+        | fortress_id (FK)    |----+   +---------------------+
                               +---------------------+        | equipment_id (PK)   |
+---------------------+                                       | name                |
| WORKSHOP_MATERIALS  |        +---------------------+        | type                |
+---------------------+        | SQUAD_MEMBERS       |        | material_id (FK)    |
| workshop_id (FK)    |        +---------------------+        | quality             |
| material_id (FK)    |        | squad_id (FK)       |----+   +---------------------+
| is_input            |        | dwarf_id (FK)       |----+
| quantity            |        | join_date           |        +---------------------+
+---------------------+        | role                |        | SQUAD_EQUIPMENT     |
                               | exit_date           |        +---------------------+
                               | exit_reason         |        | squad_id (FK)       |
                               +---------------------+        | equipment_id (FK)   |
                                                              | quantity            |
+---------------------+        +---------------------+        | issued_date         |
| WORKSHOP_PRODUCTS   |        | SQUAD_OPERATIONS    |        +---------------------+
+---------------------+        +---------------------+
| workshop_id (FK)    |        | operation_id (PK)   |
| product_id (FK)     |        | squad_id (FK)       |----+
| production_date     |        | type                |    |   +---------------------+
| quantity            |        | start_date          |    |   | SQUAD_TRAINING      |
+---------------------+        | end_date            |    |   +---------------------+
                               | status              |    |   | schedule_id (PK)    |
+---------------------+        +---------------------+    |   | squad_id (FK)       |
| PRODUCTS            |                                   |   | type                |
+---------------------+        +---------------------+    |   | frequency           |
| product_id (PK)     |        | SQUAD_BATTLES       |    |   | location_id (FK)    |
| name                |        +---------------------+    |   | effectiveness       |
| type                |        | report_id (PK)      |    |   | duration_hours      |
| quality             |        | squad_id (FK)       |----+   | date                |
| material_id (FK)    |        | date                |        +---------------------+
| value               |        | outcome             |
| created_by          |        | enemy_type          |
| workshop_id         |        | casualties          |        +---------------------+
+---------------------+        | enemy_casualties    |        | RESOURCES           |
                               +---------------------+        +---------------------+
                                                              | resource_id (PK)    |
+---------------------+        +---------------------+        | name                |
| PROJECTS            |        | EXTRACTION_SITES    |        | type                |
+---------------------+        +---------------------+        | rarity              |
| project_id (PK)     |        | site_id (PK)        |        | description         |
| name                |        | name                |        +---------------------+
| type                |        | type                |
| status              |        | resource_id (FK)    |----+
| priority            |        | depth               |    |   +---------------------+
| workshop_id (FK)    |        | accessibility       |    |   | FORTRESS_RESOURCES  |
+---------------------+        +---------------------+    |   +---------------------+
                                                          |   | fortress_id (FK)    |
+---------------------+        +---------------------+    |   | resource_id (FK)    |
| PROJECT_WORKERS     |        | PROJECT_MATERIALS   |    |   | quantity            |
+---------------------+        +---------------------+    |   | discovery_date      |
| project_id (FK)     |        | project_id (FK)     |    |   +---------------------+
| dwarf_id (FK)       |        | material_id (FK)    |----+
| assignment_date     |        | quantity_required   |
| role                |        | quantity_available  |        +---------------------+
+---------------------+        +---------------------+        | STOCKPILE_CONTENTS  |
                                                             +---------------------+
+---------------------+        +---------------------+        | stockpile_id (PK)   |
| PROJECT_DEPENDENCIES |       | PROJECT_ZONES       |        | resource_id (FK)    |
+---------------------+        +---------------------+        | quantity            |
| project_id (FK)     |        | project_id (FK)     |        | quality             |
| dependent_project_id |       | zone_id (PK)        |        +---------------------+
| dependency_type     |        | area                |
+---------------------+        | purpose             |
                               +---------------------+        +---------------------+
+---------------------+                                       | CARAVANS            |<----+
| ORDERS              |        +---------------------+        +---------------------+     |
+---------------------+        | TRADERS             |        | caravan_id (PK)     |     |
| order_id (PK)       |        +---------------------+        | arrival_date        |     |
| project_id (FK)     |        | trader_id (PK)      |        | departure_date      |     |
| description         |        | name                |        | civilization_type   |     |
| creation_date       |        | race                |        | fortress_id (FK)    |     |
| priority            |        | caravan_id (FK)     |--------+---------------------+     |
| status              |        | specialty           |                                    |
+---------------------+        +---------------------+        +---------------------+     |
                                                              | CARAVAN_GOODS       |     |
+---------------------+        +---------------------+        +---------------------+     |
| TRADE_TRANSACTIONS  |        | DWARF_INTERESTS     |        | goods_id (PK)       |     |
+---------------------+        +---------------------+        | caravan_id (FK)     |-----+
| transaction_id (PK) |        | dwarf_id (FK)       |        | name                |     |
| caravan_id (FK)     |--------| goods_type          |        | type                |     |
| date                |        | interest_level      |        | quantity            |     |
| fortress_items      |        +---------------------+        | value               |     |
| caravan_items       |                                       | material_type       |     |
| value               |        +---------------------+        | price_fluctuation   |     |
| balance_direction   |        | CREATURES           |        | original_product_id |     |
+---------------------+        +---------------------+        +---------------------+     |
                               | creature_id (PK)    |                                    |
+---------------------+        | name                |        +---------------------+     |
| EXPEDITIONS         |        | type                |        | DIPLOMATIC_EVENTS   |     |
+---------------------+        | threat_level        |        +---------------------+     |
| expedition_id (PK)  |        | active              |        | event_id (PK)       |     |
| destination         |        | estimated_population |       | caravan_id (FK)     |-----+
| departure_date      |        +---------------------+        | type                |
| return_date         |                                       | outcome             |
| status              |        +---------------------+        | date                |
+---------------------+        | CREATURE_SIGHTINGS  |        | relationship_change |
                               +---------------------+        | civilization_type   |
+---------------------+        | sighting_id (PK)    |        +---------------------+
| EXPEDITION_SITES    |        | creature_id (FK)    |
+---------------------+        | location            |        +---------------------+
| expedition_id (FK)  |        | date                |        | EXPEDITION_MEMBERS  |
| site_id (FK)        |        | witness_id (FK)     |        +---------------------+
| discovery_date      |        +---------------------+        | expedition_id (FK)  |
| notes               |                                       | dwarf_id (FK)       |
+---------------------+        +---------------------+        | role                |
                               | CREATURE_ATTACKS    |        | survived            |
+---------------------+        +---------------------+        +---------------------+
| EXPEDITION_CREATURES |       | attack_id (PK)      |
+---------------------+        | creature_id (FK)    |        +---------------------+
| expedition_id (FK)  |        | date                |        | EXPEDITION_ARTIFACTS |
| creature_id (FK)    |        | casualties          |        +---------------------+
| encounter_date      |        | enemy_casualties    |        | expedition_id (FK)  |
| outcome             |        | location_id         |        | artifact_id (PK)    |
+---------------------+        | outcome             |        | discovery_date      |
                               | defense_structures_used |    | value               |
+---------------------+        | military_response_time_minutes | +-----------------+
| EXPEDITION_EQUIPMENT |       +---------------------+
+---------------------+                                       +---------------------+
| expedition_id (FK)  |        +---------------------+        | EXPEDITION_REPORTS  |
| equipment_id (FK)   |        | CREATURE_TERRITORIES |       +---------------------+
| quantity            |        +---------------------+        | report_id (PK)      |
| return_condition    |        | territory_id (PK)   |        | expedition_id (FK)  |
+---------------------+        | creature_id (FK)    |        | author_id (FK)      |
                               | area                |        | title               |
+---------------------+        | danger_level        |        | content             |
| LOCATIONS           |        | distance_to_fortress |       | creation_date       |
+---------------------+        +---------------------+        +---------------------+
| location_id (PK)    |
| zone_id             |
| name                |
| zone_type           |
| depth               |
| access_points       |
| fortification_level |
| wall_integrity      |
| trap_density        |
| choke_points        |
+---------------------+
Получилось скорее всего неидеально :)

Основные сущности и связи:

1.Крепость (Fortresses): Основная сущность, связанная с гномами (1:n), мастерскими (1:n), военными отрядами (1:n), ресурсами (n:m)

2.Гномы (Dwarves): Принадлежат крепости (n:1), связаны с навыками (n:m), назначениями (1:n), снаряжением (n:m), интересами (1:n)

3.Навыки (Skills): Категории умений, связанные с гномами через dwarf_skills (n:m)

4.Назначения гномов (Dwarf_Assignments): Привязаны к гному (n:1), содержат информацию о рабочих задачах

5.Снаряжение гномов (Dwarf_Equipment): Связывает гномов с их личным снаряжением (n:m)

6.Мастерские (Workshops): Принадлежат крепости (n:1), включают ремесленников (n:m), материалы (n:m), производят продукты (1:n)

7.Ремесленники мастерских (Workshop_Craftsdwarves): Связывает мастерские и гномов (n:m)

8.Материалы мастерских (Workshop_Materials): Связывает мастерские с используемыми материалами (n:m)

9.Продукты мастерских (Workshop_Products): Связывает мастерские с производимыми продуктами (n:m)

10.Продукты (Products): Результаты производства, связаны с материалами (n:1), мастерскими (n:1)

11.Военные отряды (Military_Squads): Относятся к крепости (n:1), имеют лидера (n:1), членов (1:n), снаряжение (n:m)

12.Члены отрядов (Squad_Members): Связывает гномов с отрядами (n:m), содержит историю членства

13.Операции отрядов (Squad_Operations): Проводятся отрядами (n:1), содержат информацию о военных операциях

14.Тренировки отрядов (Squad_Training): Связаны с отрядами (n:1), содержат данные о тренировках

15.Сражения отрядов (Squad_Battles): Связаны с отрядами (n:1), содержат информацию о сражениях

16.Участие в битвах (Squad_Battle_Participation): Связывает отряды с атаками существ (n:m)

17.Снаряжение отрядов (Squad_Equipment): Связывает отряды со снаряжением (n:m)

18.Снаряжение (Equipment): Используется гномами (n:m) и отрядами (n:m), изготавливается из материалов (n:1)

19.Ресурсы (Resources): Добываются, используются в производстве, хранятся

20.Места добычи (Extraction_Sites): Связаны с ресурсами (n:1), содержат информацию о местах добычи

21.Ресурсы крепости (Fortress_Resources): Связывает крепости с доступными ресурсами (n:m)

22.Содержимое складов (Stockpile_Contents): Связывает склады с хранимыми ресурсами (n:m)

23.Проекты (Projects): Выполняются в мастерских (n:1), включают работников (n:m), материалы (n:m)

24.Работники проектов (Project_Workers): Связывает проекты с назначенными гномами (n:m)

25.Материалы проектов (Project_Materials): Связывает проекты с необходимыми материалами (n:m)

26.Зависимости проектов (Project_Dependencies): Определяет зависимости между проектами (n:m)

27.Зоны проектов (Project_Zones): Связывает проекты с зонами крепости (n:m)

28.Приказы (Orders): Связаны с проектами (n:1), содержат информацию о задачах

29.Караваны (Caravans): Посещают крепость, включают торговцев (1:n), товары (1:n), участвуют в транзакциях (1:n)

30.Торговцы (Traders): Принадлежат караванам (n:1), специализируются на определенных товарах

31.Товары караванов (Caravan_Goods): Связаны с караванами (n:1), представляют доступные товары. type отвечает за экспорт/импорт.

32.Торговые транзакции (Trade_Transactions): Связаны с караванами (n:1), фиксируют обмен товарами (через value)

33.Интересы гномов (Dwarf_Interests): Связаны с гномами (n:1), определяют предпочтения в товарах

34.Дипломатические события (Diplomatic_Events): Связаны с караванами (n:1), фиксируют отношения с цивилизациями

35.Существа (Creatures): Имеют тип, уровень угрозы, связаны с наблюдениями (1:n), атаками (1:n), территориями (1:n)

36.Наблюдения существ (Creature_Sightings): Фиксируют обнаружение существ (n:1)

37.Атаки существ (Creature_Attacks): Связаны с существами (n:1), локациями (n:1), структурами защиты (n:m)

38.Территории существ (Creature_Territories): Связаны с существами (n:1), определяют ареал обитания

39.Экспедиции (Expeditions): Включают участников (1:n), посещают места (1:n), обнаруживают артефакты (1:n)

40.Участники экспедиций (Expedition_Members): Связывают экспедиции с гномами (n:m)

41.Места экспедиций (Expedition_Sites): Связывают экспедиции с посещенными местами (n:m)

42.Встречи с существами (Expedition_Creatures): Связывают экспедиции с встреченными существами (n:m)

43.Снаряжение экспедиций (Expedition_Equipment): Связывает экспедиции со снаряжением (n:m)

44.Артефакты экспедиций (Expedition_Artifacts): Связаны с экспедициями (n:1), содержат данные о находках

45.Отчеты экспедиций (Expedition_Reports): Связаны с экспедициями (n:1), содержат информацию о результатах

46.Локации (Locations): Определяют места в крепости, содержат информацию о защищенности и доступности

47.Структуры защиты (Defense_Structures): Размещены в локациях (n:1), используются при обороне

48.Военные станции (Military_Stations): Связаны с отрядами (n:1), локациями (n:1), определяют размещение отрядов

49.Передвижения отрядов (Squad_Movement): Связывают отряды с патрулируемыми зонами (n:m)

50.Зоны военного покрытия (Military_Coverage_Zones): Связывают отряды с зонами и временем реакции (n:m)

51.События крепости (Fortress_Events): Фиксируют важные события, влияющие на крепость

52.Записи о погоде (Weather_Records): Содержат данные о погодных условиях в разные даты

53.Фазы луны (Moon_Phases): Фиксируют фазы луны, влияющие на игровые механики

54.Рекомендации по безопасности (Security_Recommendations): Содержат рекомендации по улучшению защиты крепости

Эта комплексная структура данных позволяет моделировать все аспекты игрового мира Dwarf Fortress, от экономики и производства до военного дела, безопасности и исследований внешнего мира.*/

/*
Для разминки: Задача 1: Получение информации о крепости с населением

Напишите SQL запрос, который возвращает данные о крепости, включая список идентификаторов всех проживающих гномов, доступных ресурсов, построенных мастерских и военных отрядов.

Важно: теперь информация из базы должна выдавать не информацию для конечного пользователя-человека, а готовый серверный ответ для REST-выдачи в формате JSON.

Для этого используйте JSON_OBJECT (функция SQL, которая строит объекты JSON из нескольких пар ключ-значение).

Что примерно выдаст REST на основании этих данных: 

[
  {
    "fortress_id": 1,
    "name": "Mountainhome",
    "location": "Eastern Mountains",
    "founded_year": 205,
    "related_entities": {
      "dwarf_ids": [101, 102, 103, 104, 105],
      "resource_ids": [201, 202, 203],
      "workshop_ids": [301, 302],
      "squad_ids": [401]
    }
  }
]
Возможное решение: */


SELECT 
    f.fortress_id,
    f.name,
    f.location,
    f.founded_year,
    JSON_OBJECT(
        'dwarf_ids', (
            SELECT JSON_ARRAYAGG(d.dwarf_id)
            FROM dwarves d
            WHERE d.fortress_id = f.fortress_id
        ),
        'resource_ids', (
            SELECT JSON_ARRAYAGG(fr.resource_id)
            FROM fortress_resources fr
            WHERE fr.fortress_id = f.fortress_id
        ),
        'workshop_ids', (
            SELECT JSON_ARRAYAGG(w.workshop_id)
            FROM workshops w
            WHERE w.fortress_id = f.fortress_id
        ),
        'squad_ids', (
            SELECT JSON_ARRAYAGG(s.squad_id)
            FROM military_squads s
            WHERE s.fortress_id = f.fortress_id
        )
    ) AS related_entities
FROM 
    fortresses f;
    
--Пока просто изучите новую базу данных и разберитесь , как работает этот SQL запрос с поддержкой формата JSON. Разберитесь самостоятельно, что такое JSON_ARRAYAGG.