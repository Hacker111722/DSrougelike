using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Player;
namespace Game.UI
{
public class ExperienceBarUI : MonoBehaviour
{
    [Header("玩家数据")]
    public PlayerStats playerStats;

    [Header("Slider")]
    public Slider expSlider;

    private void Update()
    {
        UpdateExpBar();
    }

    //���¾�����
    private void UpdateExpBar()
    {
        expSlider.maxValue = playerStats.ExpToNextLevel;
        expSlider.value = playerStats.CurrentExp;
    }

}
}