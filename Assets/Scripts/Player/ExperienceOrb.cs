using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class ExperienceOrb : MonoBehaviour
{
    [Header("经验值")]
    public int expAmount = 1;

    [Header("吸附距离")]
    public float attractDistance = 3f;

    [Header("移动速度")]
    public float moveSpeed = 5f;

    //玩家transform
    private Transform player;

    private void Start()
    {
        //查找玩家
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void AttractToPlayer()
    {
        if (player == null) return;

        //计算距离
        float distance = Vector2.Distance(transform.position, player.position);

        //进入吸附范围
        if (distance <= attractDistance)
        {
            //方向
            Vector2 direction = (player.position - transform.position).normalized;

            //移动
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果碰到玩家
        if (other.CompareTag("Player"))
        {
            //获取玩家属性组件
            PlayerStats stats = other.GetComponent<PlayerStats>();
            //增加经验
            if (stats != null)
            {
                stats.AddExperience(expAmount);
            }

            //销毁经验球
            Destroy(gameObject);
        }
    }









}