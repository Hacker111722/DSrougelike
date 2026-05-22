using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("玩家状态引用")]
    public PlayerStats playerStats;

    [Header("刷新间隔")]
    public float baseSpawnInterval = 2f;  //初始间隔是2秒
    public float minSpawnInterval = 0.3f;  //最快0.3秒
    public float intervalReducePerLevel = 0.15f;  //每级减少0.15秒

    [Header("敌人血量")]
    public int baseEnemyHealth = 3;   //初始3血
    public float healthIncreasePerLevel = 0.8f;    //每级+0.8血

    [Header("敌人速度")]
    public float baseEnemySpeed = 2f;    //初始速度
    public float speedIncreasePerLevel = 0.12f;   //每级+0.12

    [Header("最大敌人数")]
    public int baseMaxEnemies = 5;
    public int extraMaxEnemiesPerLevel =1;

    //当前难度等级 = 玩家等级-1（1级时难度为0）
    public int DifficultyLevel
    {
        get
        {
            if(playerStats == null) return 0;
            return Mathf.Max(0, playerStats.level-1);
        }
    }

    //其它系统调用的方法
    public float GetSpawnInterval()
    {
        return Mathf.Max(minSpawnInterval, baseSpawnInterval - intervalReducePerLevel*DifficultyLevel);

    }
    public int GetEnemyMaxHealth()
    {
        return baseEnemyHealth + Mathf.FloorToInt(healthIncreasePerLevel * DifficultyLevel);
    }
    public float GetEnemySpeed()
    {
        return baseEnemySpeed + speedIncreasePerLevel * DifficultyLevel;
    }
    public int GetMaxEnemies()
    {
        return baseMaxEnemies + extraMaxEnemiesPerLevel * DifficultyLevel;
    }


}
