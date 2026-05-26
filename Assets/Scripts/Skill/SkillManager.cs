using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;
using Game.Weapon;

namespace Game.Skill
{
public class SkillManager : MonoBehaviour
{
    [Header("技能列表")]
    public List<SkillData> allSkills = new List<SkillData>();

    [Header("玩家属性")]
    public PlayerStats playerStats;

    [Header("自动开火挂件")]
    public AutoShooter autoShooter;

    private void Awake()
    {
        InitSkills();
    }



    private void InitSkills()
    {
        //移动速度
        SkillData moveSpeedSkill = new SkillData();

        moveSpeedSkill.skillName = "疾风步";

        moveSpeedSkill.description = "移动速度+2";

        moveSpeedSkill.skillType = SkillType.MoveSpeed;

        moveSpeedSkill.value = 2f;

        allSkills.Add(moveSpeedSkill);

        //攻击速度
        SkillData attackSpeedSkill = new SkillData();

        attackSpeedSkill.skillName = "符咒连射";

        attackSpeedSkill.description = "使攻击速度加快";

        attackSpeedSkill.skillType = SkillType.AttackSpeed;

        attackSpeedSkill.value = 0.2f;

        allSkills.Add(attackSpeedSkill);

        //最大生命值
        SkillData healthSkill = new SkillData();

        healthSkill.skillName = "道家养生";

        healthSkill.description = "最大生命值+5";

        healthSkill.skillType = SkillType.MaxHealth;

        healthSkill.value = 5f;

        allSkills.Add(healthSkill);

        //子弹速度
        SkillData damageSkill = new SkillData();
        damageSkill.skillName = "破魔符";
        damageSkill.description = "子弹伤害+1";
        damageSkill.skillType = SkillType.Damage;
        damageSkill.value = 1f;
        allSkills.Add(damageSkill);

        //子弹速度
        SkillData bulletSpeedSkill = new SkillData();
        bulletSpeedSkill.skillName = "疾风弹";
        bulletSpeedSkill.description = "增加子弹速度";
        bulletSpeedSkill.skillType = SkillType.BulletSpeed;
        bulletSpeedSkill.value = 3f;
        allSkills.Add(bulletSpeedSkill);

        //击杀回血
        SkillData healthRegenSkill = new SkillData();
        healthRegenSkill.skillName = "饮血咒";
        healthRegenSkill.description = "击杀敌人恢复1点生命";
        healthRegenSkill.skillType = SkillType.HealthRegen;
        healthRegenSkill.value = 1f;
        allSkills.Add(healthRegenSkill);


    }

    //应用技能
    public void ApplySkill(SkillData skill)
    {
        switch (skill.skillType)
        {
            case SkillType.MoveSpeed:
                playerStats.moveSpeed += skill.value;

                PlayerController playerController = playerStats.GetComponent<PlayerController>();

                playerController.moveSpeed = playerStats.moveSpeed;
                break;

            case SkillType.AttackSpeed:
                autoShooter.attackInterval -= skill.value;

                
                autoShooter.attackInterval = Mathf.Max(0.1f, autoShooter.attackInterval);

                break;

            case SkillType.MaxHealth:
                playerStats.maxHealth += (int)skill.value;

                break;

            case SkillType.Damage:
                autoShooter.bonusDamage+=(int)skill.value;
                break;
            
            case SkillType.BulletSpeed:
                autoShooter.bonusBulletSpeed += skill.value;
                break;

            case SkillType.HealthRegen:
            //击杀回血逻辑在EnemyHealth中实现，这里只记录数值
            playerStats.healthRegenAmount += (int)skill.value;
            break;

        }

        Debug.Log("选择"+skill.skillName);

    }




}
}