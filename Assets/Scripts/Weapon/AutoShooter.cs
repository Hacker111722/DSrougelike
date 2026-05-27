using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon{
//玩家的自动开火功能
public class AutoShooter : MonoBehaviour
{
    [Header("子弹预制体")]
    [SerializeField]private GameObject bulletPrefab;
    public GameObject BulletPrefab => bulletPrefab;   //外部读取

    [Header("攻击间隔")]
    [SerializeField]private float attackInterval = 1f;
    public float AttackInterval {get=>attackInterval; set=>attackInterval = value;}

    [Header("攻击距离")]
    [SerializeField]private float attackRange = 8f;

    //缓存的敌人列表，避免每次攻击都Find
    private GameObject[] cachedEnemies;

    //敌人缓存刷新计时器
    private float enemyCacheTimer = 0f;
    
    //敌人缓存刷新间隔（秒）
    //经验值：0.3~0.5秒刷新一次就够了，敌人是连续追赶玩家的
    //不需要每帧都精确知道每个敌人的位置
    private const float ENEMY_CACHE_INTERVAL = 0.3f;

    public int BonusDamage{get; set;}    //额外子弹伤害
    public float BonusBulletSpeed{get;set;}    //额外子弹速度


    //计时器
    private float timer;

    private void Update()
    {
        //定时刷新敌人缓存（独立于攻击计时器）
        //即使没有攻击，缓存也会刷新，这样攻击时拿到的就是最新数据
        enemyCacheTimer += Time.deltaTime;
        if(enemyCacheTimer >= ENEMY_CACHE_INTERVAL)
        {
            enemyCacheTimer = 0f;
            //只在需要刷新时才调用Find,而不是每次攻击时调用
            //这样把O(n)操作从攻击频率降低到固定频率
            cachedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        //攻击计时器
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
        //用缓存替代Find,如果缓存为空或没有敌人，直接返回
        if(cachedEnemies == null || cachedEnemies.Length==0) return;

        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        //遍历缓存的敌人列表
        //敌人可能在缓存刷新间隙被销毁（被子弹打死），所以需要null检查
        foreach(GameObject enemy in cachedEnemies)
        {
            if(enemy == null) continue;   //被销毁的敌人，跳过
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if(distance <= attackRange && distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy == null) return;
        

        //得到子弹发射方向
        Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;

        //实例化子弹
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //初始化子弹
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage += BonusDamage;    //应用额外伤害
        bulletScript.moveSpeed += BonusBulletSpeed;    //应用额外子弹速度
        bulletScript.Init(direction);

    }
}
}
