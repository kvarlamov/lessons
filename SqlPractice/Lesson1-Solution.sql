-- 1. Получить информацию о всех гномах, которые входят в какой-либо отряд, вместе с информацией об их отрядах.

SELECT D.name       AS DwarfName,
       D.profession AS Profession,
       S.name       AS SquadName,
       S.mission    AS Mission
FROM Dwarves D
         JOIN
     Squads S
     ON
         D.squad_id = S.squad_id;

-- 2. Найти всех гномов с профессией "miner", которые не состоят ни в одном отряде.

SELECT name,
       age
FROM Dwarves
WHERE profession = 'miner'
  AND squad_id IS NULL;

-- 3. Получить все задачи с наивысшим приоритетом, которые находятся в статусе "pending".

SELECT task_id,
       description,
       assigned_to
FROM Tasks
WHERE priority = (SELECT MAX(priority) FROM Tasks WHERE status = 'pending')
  AND status = 'pending';

-- 4. Для каждого гнома, который владеет хотя бы одним предметом, получить количество предметов, которыми он владеет.

SELECT D.name           AS DwarfName,
       D.profession     AS Profession,
       COUNT(I.item_id) AS ItemCount
FROM Dwarves D
         JOIN
     Items I
     ON
         D.dwarf_id = I.owner_id
GROUP BY D.dwarf_id, D.name, D.profession;

-- 5. Получить список всех отрядов и количество гномов в каждом отряде. Также включите в выдачу отряды без гномов.

SELECT S.squad_id,
       S.name            AS SquadName,
       COUNT(D.dwarf_id) AS NumberOfDwarves
FROM Squads S
         LEFT JOIN
     Dwarves D
     ON
         S.squad_id = D.squad_id
GROUP BY S.squad_id, S.name;

-- 6. Получить список профессий с наибольшим количеством незавершённых задач ("pending" и "in_progress") у гномов этих профессий.

SELECT D.profession,
       COUNT(T.task_id) AS UnfinishedTasksCount
FROM Dwarves D
         JOIN
     Tasks T
     ON
         D.dwarf_id = T.assigned_to
WHERE T.status IN ('pending', 'in_progress')
GROUP BY D.profession
ORDER BY UnfinishedTasksCount DESC;

-- 7. Для каждого типа предметов узнать средний возраст гномов, владеющих этими предметами.

SELECT I.type     AS ItemType,
       AVG(D.age) AS AverageAge
FROM Items I
         JOIN
     Dwarves D
     ON
         I.owner_id = D.dwarf_id
GROUP BY I.type;

-- 8. Найти всех гномов старше среднего возраста (по всем гномам в базе), которые не владеют никакими предметами.

SELECT D.name,
       D.age,
       D.profession
FROM Dwarves D
WHERE D.age > (SELECT AVG(age) FROM Dwarves)
  AND D.dwarf_id NOT IN (SELECT owner_id FROM Items);