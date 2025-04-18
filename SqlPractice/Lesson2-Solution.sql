-- 1. Найдите все отряды, у которых нет лидера.
    SELECT *
    FROM Squads
    WHERE leader_id IS NULL;

-- 2. Получите список всех гномов старше 150 лет, у которых профессия "Warrior".
    SELECT *
    FROM Dwarves
    WHERE age > 150 AND profession = 'Warrior';

-- 3. Найдите гномов, у которых есть хотя бы один предмет типа "weapon".
    SELECT DISTINCT D.*
    FROM Dwarves D
    JOIN Items I ON D.dwarf_id = I.owner_id
    WHERE I.type = 'weapon';

-- 4. Получите количество задач для каждого гнома, сгруппировав их по статусу.
    SELECT assigned_to, status, COUNT(*) AS task_count
    FROM Tasks
    GROUP BY assigned_to, status;

-- 5. Найдите все задачи, которые были назначены гномам из отряда с именем "Guardians".
    SELECT T.*
    FROM Tasks T
    JOIN Dwarves D ON T.assigned_to = D.dwarf_id
    JOIN Squads S ON D.squad_id = S.squad_id
    WHERE S.name = 'Guardians';

-- 6. Выведите всех гномов и их ближайших родственников, указав тип родственных отношений.
    SELECT D1.name AS dwarf_name, D2.name AS relative_name, R.relationship
    FROM Relationships R
    JOIN Dwarves D1 ON R.dwarf_id = D1.dwarf_id
    JOIN Dwarves D2 ON R.related_to = D2.dwarf_id;