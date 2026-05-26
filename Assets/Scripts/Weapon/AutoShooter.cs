using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon{
//玩家的自动开火功能
public class AutoShooter : MonoBehaviour
{
    [Header("子弹预制体")]
    public GameObject bulletPrefab;

    [Header("攻击间隔")]
    public float attackInterval = 1f;

    [Header("攻击距离")]
    public float attackRange = 8f;

    [HideInInspector] public int bonusDamage = 0;    //额外子弹伤害
    [HideInInspector] public float bonusBulletSpeed = 0f;    //额外子弹速度


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

    //攻击距离最近的敌人
    private void AttackNearestEnemy()
    {
        //识别敌人
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //如果没敌人
        if (enemies.Length == 0) return;

        GameObject nearestEnemy = null;

        float nearestDistance = Mathf.Infinity;

        //找到最近的敌人
        foreach (GameObject enemy in enemies)
        {
            //计算距离
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            //判断是否进入范围
            if (distance <= attackRange) {
                //找到最近的敌人
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy == null) return;
        //得到子弹发射方向
        Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;

        //实例化子弹
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //初始化子弹
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage += bonusDamage;    //应用额外伤害
        bulletScript.moveSpeed += bonusBulletSpeed;    //应用额外子弹速度
        bulletScript.Init(direction);

    }
}
}
