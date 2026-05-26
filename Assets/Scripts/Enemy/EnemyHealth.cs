using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;
namespace Game.Enemy
{

//敌人的生命脚本
public class EnemyHealth : MonoBehaviour
{
    [Header("经验球预制体")]
    public GameObject expOrPrefab;

    [Header("最大生命值")]
    public int maxHealth = 3;

    [Header("碰撞伤害")]
    public int contactDamage = 1;

    [Header("爆炸参数（自爆用）")]
    public float explosionRadius = 0f;
    [HideInInspector] public int explosionDamage = 2;

    //当前血量
    private int currentHealth;
    //判断是否死亡
    private bool hasDied = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    //收到伤害
    public void TakeDamage(int damage)
    {
        //如果死亡则返回
        if(hasDied) return;
        currentHealth -= damage;
        //死亡逻辑
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(hasDied) return;
        hasDied = true;
        //自爆敌人，先播放动画预警，再爆炸
        if(explosionRadius>0f)
        {
            StartCoroutine(ExplodeWithWarning());
        }
        else
        {
            //普通死亡，直接销毁
            Instantiate(expOrPrefab, transform.position, Quaternion.identity);
            //触发玩家击杀回血
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            if(playerStats!=null && playerStats.healthRegenAmount>0)
            {
                playerStats.currentHealth+= playerStats.healthRegenAmount;
                playerStats.currentHealth = Mathf.Min(playerStats.currentHealth, playerStats.maxHealth);
            }        
            Destroy(gameObject);
        }


    }

    private IEnumerator ExplodeWithWarning()
    {
        //触发预警动画
        Animator animator = GetComponent<Animator>();
        if(animator !=null)
        {
            animator.SetTrigger("Explode");
        }

        //等待预警动画播放完
        yield return new WaitForSeconds(2f);

        //生成经验球
        Instantiate(expOrPrefab, transform.position, Quaternion.identity);

        //玩家回血
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if(playerStats != null && playerStats.healthRegenAmount>0)
        {
            playerStats.currentHealth += playerStats.healthRegenAmount;
            playerStats.currentHealth = Mathf.Min(playerStats.currentHealth, playerStats.maxHealth);
        }

        //爆炸伤害周围敌人
        if(explosionRadius > 0f)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach(var hit in hitColliders)
            {
                EnemyHealth otherEnemy = hit.GetComponent<EnemyHealth>();
                if(otherEnemy != null && otherEnemy != this)
                {
                    otherEnemy.TakeDamage(explosionDamage);
                }
            }
        }

        Destroy(gameObject);

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
}

