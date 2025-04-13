/*
Таблица Dwarves

| Field        | Type         | Description                               |
|--------------|--------------|-------------------------------------------|
| dwarf_id     | INT          | Уникальный идентификатор гнома            |
| name         | VARCHAR(100) | Имя гнома                                 |
| age          | INT          | Возраст гнома                             |
| profession   | VARCHAR(100) | Профессия гнома                           |
| squad_id     | INT          | Идентификатор отряда                      
                                (NULL, если не в отряде)                  |

Таблица Squads

| Field        | Type         | Description                               |
|--------------|--------------|-------------------------------------------|
| squad_id     | INT          | Уникальный идентификатор отряда           |
| name         | VARCHAR(100) | Название отряда                           |
| mission      | VARCHAR(100) | Текущая миссия отряда                     |

Таблица Tasks

| Field        | Type         | Description                               |
|--------------|--------------|-------------------------------------------|
| task_id      | INT          | Уникальный идентификатор задачи           |
| description  | VARCHAR(255) | Описание задачи                           |
| priority     | INT          | Приоритет задачи                          |
| assigned_to  | INT          | Идентификатор гнома, ответственного за задачу 
                                (NULL, если не назначена)                 |
| status       | VARCHAR(50)  | Статус задачи                             
                        (например, 'pending', 'in_progress', 'completed') |

Таблица Items

| Field        | Type         | Description                               |
|--------------|--------------|-------------------------------------------|
| item_id      | INT          | Уникальный идентификатор предмета         |
| name         | VARCHAR(100) | Название предмета                         |
| type         | VARCHAR(100) | Тип предмета 
                                (например, 'weapon', 'armor', 'tool')     |
| owner_id     | INT          | Идентификатор гнома-владельца 
                                (NULL, если предмет общий)                |
Конечно, типы, статусы, профессии и т.д. лучше задавать числовыми идентификаторами, однако во множестве легаси-проектов не очень корректно используются строки.
*/

-- 1. Получить информацию о всех гномах, которые входят в какой-либо отряд, вместе с информацией об их отрядах.
select 
    d.*, s.name, s.mission
from 
    Dwarves as d
inner join Squads as s 
    on d.squad_id=s.squad_id;

-- 2. Найти всех гномов с профессией "miner", которые не состоят ни в одном отряде.
select
    *
from
    Dwarves
where
    profession = 'miner'
    and squad_id is null;

-- 3. Получить все задачи с наивысшим приоритетом, которые находятся в статусе "pending".
select
    *
from
    Tasks
where
    status='pending'
    and priority = (select max(priority) from Tasks);

-- 4. Для каждого гнома, который владеет хотя бы одним предметом, получить количество предметов, которыми он владеет.
with items_with_owners as (
    select
        *
    from
        Items
    where
        owner_id is not null
)
select 
    owner_id, count(item_id)
from
    items_with_owners i
group by 
    i.owner_id;

-- 5. Получить список всех отрядов и количество гномов в каждом отряде. Также включите в выдачу отряды без гномов.
select
    s.name, count(d.squad_id)
from
    Squads as s
left join
    Dwarws as d
on 
    s.squad_id=d.squad_id
group by 
    s.name;

-- 6. Получить список профессий с наибольшим количеством незавершённых задач ("pending" и "in_progress") у гномов этих профессий.
with not_finished_tasks as (
    select 
        * 
    from 
        Tasks 
    where 
        status in ('pending', 'in_progress')
)
select
    d.profession,
    COUNT(t.task_id)
from
    Dwarves as d
inner join
    not_finished_tasks as t
on 
    d.dwarf_id=t.assigned_to
group by 
    d.profession;

-- 7. Для каждого типа предметов узнать средний возраст гномов, владеющих этими предметами.
select
    i.type,
    avg(d.age)
from
    Items i
inner join 
    Dwarves d on d.dwarf_id = i.owner_id
group by 
    i.type;

-- 8. Найти всех гномов старше среднего возраста (по всем гномам в базе), которые не владеют никакими предметами.
WITH average_age AS (SELECT AVG(age) AS avg_age FROM Dwarves)
SELECT
    d.*
FROM
    Dwarves d
        LEFT JOIN Items i
ON d.dwarf_id = i.owner_id
WHERE
    d.age > (SELECT avg_age FROM average_age)
    AND i.owner_id IS NULL;
    