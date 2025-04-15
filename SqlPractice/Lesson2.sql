/*Таблица Dwarves

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
| leader_id    | INT          | Идентификатор лидера отряда 
                                (ссылка на dwarf_id из таблицы Dwarves)   |

Таблица Tasks

| Field        | Type         | Description                               |
|--------------|--------------|-------------------------------------------|
| task_id      | INT          | Уникальный идентификатор задачи           |
| description  | TEXT         | Описание задачи                           |
| assigned_to  | INT          | Идентификатор гнома, ответственного за задачу 
                                (NULL, если не назначена)                 |
| status       | VARCHAR(50)  | Статус задачи                             
                        (например, 'pending', 'in_progress', 'completed') |

Таблица Items

| Field        | Type         | Description                               |
|--------------|--------------|-------------------------------------------|
| item_id      | INT          | Уникальный идентификатор предмета         |
| name         | VARCHAR(100) | Название предмета                         |
| type         | VARCHAR(50)  | Тип предмета 
                                (например, 'weapon', 'armor', 'tool')     |
| owner_id     | INT          | Идентификатор гнома-владельца 
                                (ссылка на dwarf_id из таблицы Dwarves, 
                                или NULL если не присвоено)               |

Таблица Relationships


| Field        | Type         | Description                               |
|--------------|--------------|-------------------------------------------|
| dwarf_id     | INT          | Уникальный идентификатор гнома 
                                (ссылка на dwarf_id)                      |
| related_to   | INT          | Идентификатор другого гнома 
                                (ссылка на dwarf_id)                      |
| relationship | VARCHAR(50)  | Тип отношения 
                                (например, 'Друг', 'Супруг', 'Родитель')  |

 */


-- 1. Найдите все отряды, у которых нет лидера.
select
    s.name, s.squad_id
from 
    Squads s
left join Dwarves d on 
    s.leader_id = d.dwarf_id
where
    d.dwarа_id is null;
    
-- 2. Получите список всех гномов старше 150 лет, у которых профессия "Warrior".
select 
    d.name, d.age, d.profession
from Dwarves d
where 
    d.age > 150
    and d.profession = 'Warrior';

-- 3. Найдите гномов, у которых есть хотя бы один предмет типа "weapon".
select distinct 
    d.name, d.age, d.profession
from Dwarves d
    inner join  Items i
    on d.dwarf_id = i.owner_id
where
    i.type='weapon';

-- 4. Получите количество задач для каждого гнома, сгруппировав их по статусу.
select
    d.name, t.status, count(t.task_id)
from
    Dwarves d
inner join
    Tasks t
on d.dwarf_id = t.assigned_to
group by 
    d.name, t.status;

-- 5. Найдите все задачи, которые были назначены гномам из отряда с именем "Guardians".
select
    t.task_id, t.description
from Tasks t
inner join
    Dwarves d on d.dwarf_id = t.assigned_to
inner join
    Squads s on s.squad_id=d.squad_id
where
    s.name='Guardians';

-- 6. Выведите всех гномов и их ближайших родственников, указав тип родственных отношений.
select
    d1.name, d2.name, r1.relationship
from Relationships r1
inner join Dwarves d1
on d1.dwarf_id = r1.dwarf_id
inner join Dwarves d2
on d2.dwarf_id = r1.related_to;