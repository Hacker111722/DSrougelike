using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人生命系统
public class EnemyHealth : MonoBehaviour
{
    [Header("经验球预制体")]
    public GameObject expOrPrefab;

    [Header("最大生命值")]
    public int maxHealth = 3;
    //当前生命值
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    //受到伤害
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //死亡检测
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(expOrPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }




}
