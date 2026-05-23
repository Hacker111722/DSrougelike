using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("敌人预制体")]
    public GameObject enemyPrefab;

    [Header("敌人类型配置")]
    public List<EnemyConfig> enemyConfigs = new List<EnemyConfig>();

    [Header("敌人生成时间间隔")]
    public float spawnInterval = 2f;

    [Header("生成半径")]
    public float spawnRadius = 10f;

    [Header("场上敌人的最大数量")]
    public float maxNumberofEnemy;

    [Header("难度管理器")]
    public DifficultyManager difficultyManager;

    //场上当前敌人数量
    private int currentEnemyCount;

    private float timer;

    private void Update()
    {
        //每次更新从难度管理器获取最新间隔
        float currentInterval = spawnInterval;
        if(difficultyManager != null)
        {
            currentInterval = difficultyManager.GetSpawnInterval();
        }
        timer += Time.deltaTime;
        //当计时器大于生成时间时生成敌人并归零计时器
        if (timer > currentInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        //检查是否达到了最大敌人数量
        int maxEnemies = difficultyManager != null ? difficultyManager.GetMaxEnemies() :(int)maxNumberofEnemy;
        
        //统计场上敌人
        GameObject[] currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (currentEnemies.Length >= maxEnemies) return;

        //获取当前玩家等级
        int playerLevel = difficultyManager != null && difficultyManager.playerStats !=null ? difficultyManager.playerStats.level : 1;


        //筛选出当前等级可用的敌人类型
        List<EnemyConfig> availableConfigs = new List<EnemyConfig>();
        foreach (var config in enemyConfigs)
        {
            if(playerLevel >= config.minPlayerLevel)
            {
                availableConfigs.Add(config);
            }
        }

        if(availableConfigs.Count==0) return;

        //随机选一种敌人类型
        EnemyConfig selectedConfig = availableConfigs[Random.Range(0, availableConfigs.Count)];
        
        
        //生成随机方位方向
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        //在得到敌人的随机生成位置
        Vector2 spawnPosition = (Vector2)transform.position + randomDirection * spawnRadius;

        //生成敌人
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        //应用类型配置
        //缩放体型
        enemy.transform.localScale = Vector3.one * selectedConfig.sizeScale;

        //修改颜色
        SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
        if(sr != null)
        {
            sr.color = selectedConfig.spriteColor;
        }

        //配置速度
        EnemyController controller = enemy.GetComponent<EnemyController>();
        if(controller != null)
        {
            controller.moveSpeed = difficultyManager != null ? difficultyManager.GetEnemySpeed() * (selectedConfig.moveSpeed/2f) : selectedConfig.moveSpeed;
        }

        //配置血量
        EnemyHealth health  = enemy.GetComponent<EnemyHealth>();
        if(health != null)
        {
            health.maxHealth = difficultyManager != null ? difficultyManager.GetEnemyMaxHealth() + (selectedConfig.maxHealth - 3) : selectedConfig.maxHealth;
            health.contactDamage = selectedConfig.contactDamage;
            health.explosionRadius = selectedConfig.explosionRadius;
            health.explosionDamage = selectedConfig.contactDamage * 2;
            health.ResetHealth();
        }

        //记录敌人类型
        EnemyTypeHolder typeHolder = enemy.GetComponent<EnemyTypeHolder>();
        if(typeHolder != null)
        {
            typeHolder.enemyType = selectedConfig.enemyType;
        }

        

    }


}
