using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������ϵͳ
public class EnemyHealth : MonoBehaviour
{
    [Header("经验球预制体")]
    public GameObject expOrPrefab;

    [Header("最大生命值")]
    public int maxHealth = 3;
    //当前血量
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    //收到伤害
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //死亡逻辑
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(expOrPrefab, transform.position, Quaternion.identity);
        //触发玩家击杀回血
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if(playerStats != null && playerStats.healthRegenAmount>0)
        {
            playerStats.currentHealth += playerStats.healthRegenAmount;
            playerStats.currentHealth = Mathf.Min(playerStats.currentHealth,playerStats.maxHealth);
        } 

        Destroy(gameObject);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }




}
