using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("敌人预制体")]
    public GameObject enemyPrefab;

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
        if (timer > spawnInterval)
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
        
        
        //生成随机方位方向
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        //在得到敌人的随机生成位置
        Vector2 spawnPosition = (Vector2)transform.position + randomDirection * spawnRadius;

        //生成敌人
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        //进行难度缩放
        if(difficultyManager !=null)
        {
            //血量缩放
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if(health != null)
            {
                health.maxHealth = difficultyManager.GetEnemyMaxHealth();
                //重新初始化当前血量
                health.ResetHealth();
            }

            //速度缩放
            EnemyController controller = enemy.GetComponent<EnemyController>();
            if(controller != null)
            {
                controller.moveSpeed = difficultyManager.GetEnemySpeed();
            }
        }

    }


}
