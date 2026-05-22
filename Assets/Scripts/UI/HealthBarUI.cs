
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("玩家状态")]
    public PlayerStats playerStats;

    [Header("血量 Slider")]
    public Slider healthSlider;

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if(playerStats == null || healthSlider==null) return;

        healthSlider.maxValue = playerStats.maxHealth;
        healthSlider.value = playerStats.currentHealth;
    }


}
