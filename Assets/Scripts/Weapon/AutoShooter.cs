using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自动射击系统
public class AutoShooter : MonoBehaviour
{
    [Header("子弹预制体")]
    public GameObject bulletPrefab;

    [Header("攻击间隔")]
    public float attackInterval = 1f;

    [Header("攻击范围")]
    public float attackRange = 8f;

    //计时器
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackInterval)
        {
            timer = 0f;
            AttackNearestEnemy();
        }

    }

    //攻击最近的敌人
    private void AttackNearestEnemy()
    {
        //获取所有敌人
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //没有敌人
        if (enemies.Length == 0) return;

        GameObject nearestEnemy = null;

        float nearestDistance = Mathf.Infinity;

        //遍历所有敌人
        foreach (GameObject enemy in enemies)
        {
            //计算距离
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            //在攻击范围内
            if (distance <= attackRange) {
                //找更近的敌人
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy == null) return;
        //计算方向
        Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;

        //生成子弹
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //获取子弹组件
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Init(direction);

    }




}
