using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Skill
{
public class SkillSelectionUI : MonoBehaviour
{
    [Header("技能选择按钮")]
    public List<SkillButtonUI> skillButton;

    [Header("技能管理")]
    public SkillManager skillManager;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowSkillSelection()
    {
        gameObject.SetActive(true);

        

        //得到随机技能
        List<SkillData> randomSkills = GetRandomSkills(3);

        //初始化按钮
        for (int i = 0; i < skillButton.Count; i++)
        {
            if(i<randomSkills.Count)
            {
                skillButton[i].gameObject.SetActive(true);
                skillButton[i].Init(randomSkills[i],this);
    
            }else
            {
                skillButton[i].gameObject.SetActive(false);
            }
        }
        

        //暂停游戏
        Time.timeScale = 0f;

    }

    private List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> result = new List<SkillData>();

        //防御性检查，防止allSkills为空导致越界
        if(skillManager == null || skillManager.allSkills ==null)
        {
            Debug.LogError("没有可选技能！skillManager.allSkills为空");
            return result;
        }

        List<SkillData> tempList = new List<SkillData>(skillManager.allSkills);

        //实际取技能数 = 最少（想要的数量，可用技能数）
        int actualCount = Mathf.Min(count, tempList.Count);

        for(int i=0;i<actualCount;i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            result.Add(tempList[randomIndex]);
            //选完后从临时列表移除，避免重复选中同一个技能
            tempList.RemoveAt(randomIndex);
        }
        return result;



    }

    public void SelectSkill(SkillData skill)
    {
        skillManager.ApplySkill(skill);
        //使游戏继续
        Time.timeScale = 1f;
        gameObject.SetActive(false); 
    }
}
}
