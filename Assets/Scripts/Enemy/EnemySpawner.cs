using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("敌人预制体")]
    public GameObject enemyPrefab;

    [Header("生成间隔")]
    public float spawnInterval = 2f;

    [Header("生成半径")]
    public float spawnRadius = 10f;

    [Header("场内最大敌人数量")]
    public float maxNumberofEnemy;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        //达到生成时间
        if (timer > spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        //随机方向
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        //生成位置
        Vector2 spawnPosition = (Vector2)transform.position + randomDirection * spawnRadius;

        //生成敌人
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    }


}
