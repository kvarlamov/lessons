WITH squad_battle_stats AS (
    SELECT 
        sb.squad_id,
        COUNT(sb.report_id) AS total_battles,
        SUM(CASE WHEN sb.outcome = 'Victory' THEN 1 ELSE 0 END) AS victories,
        SUM(CASE WHEN sb.outcome = 'Defeat' THEN 1 ELSE 0 END) AS defeats,
        SUM(CASE WHEN sb.outcome = 'Retreat' THEN 1 ELSE 0 END) AS retreats,
        SUM(sb.casualties) AS total_casualties,
        SUM(sb.enemy_casualties) AS total_enemy_casualties,
        MIN(sb.date) AS first_battle,
        MAX(sb.date) AS last_battle
    FROM 
        squad_battles sb
    GROUP BY 
        sb.squad_id
),
squad_member_history AS (
    SELECT 
        sm.squad_id,
        COUNT(DISTINCT sm.dwarf_id) AS total_members_ever,
        COUNT(DISTINCT CASE WHEN sm.exit_reason IS NULL THEN sm.dwarf_id END) AS current_members,
        COUNT(DISTINCT CASE WHEN sm.exit_reason = 'Death' THEN sm.dwarf_id END) AS deaths,
        AVG(EXTRACT(DAY FROM (COALESCE(sm.exit_date, CURRENT_DATE) - sm.join_date))) AS avg_service_days
    FROM 
        squad_members sm
    GROUP BY 
        sm.squad_id
),
squad_skill_progression AS (
    SELECT 
        sm.squad_id,
        sm.dwarf_id,
        AVG(ds_current.level - ds_join.level) AS avg_skill_improvement,
        MAX(ds_current.level) AS max_current_skill
    FROM 
        squad_members sm
    JOIN 
        dwarf_skills ds_join ON sm.dwarf_id = ds_join.dwarf_id AND ds_join.date <= sm.join_date
    JOIN 
        dwarf_skills ds_current ON sm.dwarf_id = ds_current.dwarf_id 
        AND ds_current.skill_id = ds_join.skill_id
        AND ds_current.date = (
            SELECT MAX(date) 
            FROM dwarf_skills 
            WHERE dwarf_id = sm.dwarf_id AND skill_id = ds_join.skill_id
        )
    JOIN 
        skills s ON ds_join.skill_id = s.skill_id
    WHERE 
        s.category IN ('Combat', 'Military')
    GROUP BY 
        sm.squad_id, sm.dwarf_id
),
squad_equipment_quality AS (
    SELECT 
        se.squad_id,
        AVG(e.quality::INTEGER) AS avg_equipment_quality,
        MIN(e.quality::INTEGER) AS min_equipment_quality,
        COUNT(DISTINCT e.equipment_id) AS unique_equipment_count,
        SUM(CASE WHEN e.type = 'Weapon' THEN 1 ELSE 0 END) AS weapon_count,
        SUM(CASE WHEN e.type = 'Armor' THEN 1 ELSE 0 END) AS armor_count,
        SUM(CASE WHEN e.type = 'Shield' THEN 1 ELSE 0 END) AS shield_count
    FROM 
        squad_equipment se
    JOIN 
        equipment e ON se.equipment_id = e.equipment_id
    GROUP BY 
        se.squad_id
),
squad_training_effectiveness AS (
    SELECT 
        st.squad_id,
        COUNT(st.schedule_id) AS total_training_sessions,
        AVG(st.effectiveness::DECIMAL) AS avg_training_effectiveness,
        SUM(st.duration_hours) AS total_training_hours,
        -- Calculate if training improves battle outcomes
        CORR(
            st.effectiveness::DECIMAL,
            CASE WHEN sb.outcome = 'Victory' THEN 1 ELSE 0 END
        ) AS training_battle_correlation
    FROM 
        squad_training st
    LEFT JOIN 
        squad_battles sb ON st.squad_id = sb.squad_id AND sb.date > st.date
    GROUP BY 
        st.squad_id
)
SELECT 
    s.squad_id,
    s.name AS squad_name,
    s.formation_type,
    -- Leadership effectiveness
    d.name AS leader_name,
    sbs.total_battles,
    sbs.victories,
    sbs.defeats,
    ROUND((sbs.victories::DECIMAL / NULLIF(sbs.total_battles, 0)) * 100, 2) AS victory_percentage,
    ROUND((sbs.total_casualties::DECIMAL / NULLIF(smh.total_members_ever, 0)) * 100, 2) AS casualty_rate,
    ROUND((sbs.total_enemy_casualties::DECIMAL / NULLIF(sbs.total_casualties, 1)), 2) AS casualty_exchange_ratio,
    -- Member stats
    smh.current_members,
    smh.total_members_ever,
    ROUND((smh.current_members::DECIMAL / NULLIF(smh.total_members_ever, 0)) * 100, 2) AS retention_rate,
    smh.avg_service_days,
    -- Equipment effectiveness
    seq.avg_equipment_quality,
    seq.min_equipment_quality,
    seq.weapon_count + seq.armor_count + seq.shield_count AS total_equipment_pieces,
    -- Training effectiveness
    ste.total_training_sessions,
    ste.total_training_hours,
    ste.avg_training_effectiveness,
    ste.training_battle_correlation,
    -- Skill progression
    ROUND(AVG(ssp.avg_skill_improvement), 2) AS avg_combat_skill_improvement,
    ROUND(AVG(ssp.max_current_skill), 2) AS avg_max_combat_skill,
    -- Years active
    EXTRACT(YEAR FROM (sbs.last_battle - sbs.first_battle)) AS years_active,
    -- Overall effectiveness score
    ROUND(
        (sbs.victories::DECIMAL / NULLIF(sbs.total_battles, 0)) * 0.25 +
        (1 - (sbs.total_casualties::DECIMAL / NULLIF(smh.total_members_ever, 0))) * 0.20 +
        (smh.current_members::DECIMAL / NULLIF(smh.total_members_ever, 0)) * 0.15 +
        (seq.avg_equipment_quality::DECIMAL / 5) * 0.15 +
        (ste.avg_training_effectiveness) * 0.15 +
        (AVG(ssp.avg_skill_improvement) / 5) * 0.10,
        3
    ) AS overall_effectiveness_score,
    -- Related entities for REST API
    JSON_OBJECT(
        'member_ids', (
            SELECT JSON_ARRAYAGG(sm.dwarf_id)
            FROM squad_members sm
            WHERE sm.squad_id = s.squad_id AND sm.exit_date IS NULL
        ),
        'equipment_ids', (
            SELECT JSON_ARRAYAGG(se.equipment_id)
            FROM squad_equipment se
            WHERE se.squad_id = s.squad_id
        ),
        'battle_report_ids', (
            SELECT JSON_ARRAYAGG(sb.report_id)
            FROM squad_battles sb
            WHERE sb.squad_id = s.squad_id
        ),
        'training_ids', (
            SELECT JSON_ARRAYAGG(st.schedule_id)
            FROM squad_training st
            WHERE st.squad_id = s.squad_id
        )
    ) AS related_entities
FROM 
    military_squads s
JOIN 
    dwarves d ON s.leader_id = d.dwarf_id
LEFT JOIN 
    squad_battle_stats sbs ON s.squad_id = sbs.squad_id
LEFT JOIN 
    squad_member_history smh ON s.squad_id = smh.squad_id
LEFT JOIN 
    squad_equipment_quality seq ON s.squad_id = seq.squad_id
LEFT JOIN 
    squad_training_effectiveness ste ON s.squad_id = ste.squad_id
LEFT JOIN 
    squad_skill_progression ssp ON s.squad_id = ssp.squad_id
GROUP BY 
    s.squad_id, s.name, s.formation_type, d.name, 
    sbs.total_battles, sbs.victories, sbs.defeats, sbs.total_casualties,
    sbs.total_enemy_casualties, sbs.first_battle, sbs.last_battle,
    smh.current_members, smh.total_members_ever, smh.avg_service_days,
    seq.avg_equipment_quality, seq.min_equipment_quality, seq.weapon_count,
    seq.armor_count, seq.shield_count,
    ste.total_training_sessions, ste.total_training_hours, 
    ste.avg_training_effectiveness, ste.training_battle_correlation
ORDER BY 
    overall_effectiveness_score DESC;