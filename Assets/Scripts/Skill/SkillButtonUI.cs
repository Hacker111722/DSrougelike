using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillButtonUI : MonoBehaviour
{
    [Header("技能名称文本")]
    public TextMeshProUGUI skillNameText;

    [Header("技能描述文本")]
    public TextMeshProUGUI skillDescriptionText;

    private SkillData currentSkill;

    private SkillSelectionUI selectionUI;

    public void Init(SkillData skill, SkillSelectionUI ui)
    {
        currentSkill = skill;

        selectionUI = ui;

        skillNameText.text = skill.skillName;

        skillDescriptionText.text = skill.description;


    }

    public void OnClick()
    {
        selectionUI.SelectSkill(currentSkill);
    }




}
