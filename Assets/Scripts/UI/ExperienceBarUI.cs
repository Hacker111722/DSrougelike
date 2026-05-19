using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarUI : MonoBehaviour
{
    [Header("玩家属性")]
    public PlayerStats playerStats;

    [Header("Slider")]
    public Slider expSlider;

    private void Update()
    {
        UpdateExpBar();
    }

    //更新经验条
    private void UpdateExpBar()
    {
        expSlider.maxValue = playerStats.expToNextLevel;
        expSlider.value = playerStats.currentExp;
    }

}
