using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    [Header("技能名称")]
    public string skillName;

    [Header("技能描述")]
    public string description;

    [Header("技能类型")]
    public SkillType skillType;

    [Header("技能数值")]
    public float value;
}

public enum SkillType
{
    MoveSpeed,
    AttackSpeed,
    MaxHealth,
    Damage,
    BulletSpeed,
    HealthRegen
}