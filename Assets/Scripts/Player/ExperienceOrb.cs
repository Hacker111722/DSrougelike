using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
namespace Game.Player
{
public class ExperienceOrb : MonoBehaviour
{
    [Header("拾取经验球后获得的经验")]
    public int expAmount = 1;

    [Header("经验球拾取范围")]
    public float attractDistance = 3f;

    [Header("经验球吸取速度")]
    public float moveSpeed = 5f;

    //玩家的transform
    private Transform player;

    private void Start()
    {
        //获取玩家的transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void AttractToPlayer()
    {
        if (player == null) return;

        //计算经验球与玩家之间的距离
        float distance = Vector2.Distance(transform.position, player.position);

        //设定拾取经验球的逻辑
        if (distance <= attractDistance)
        {
            //单位化方向向量
            Vector2 direction = (player.position - transform.position).normalized;

            //令经验球冲向玩家
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //设定经验球的碰撞逻辑
        if (other.CompareTag("Player"))
        {
            //获取玩家状态
            PlayerStats stats = other.GetComponent<PlayerStats>();
            //判定玩家是否能加经验值
            if (stats != null)
            {
                stats.AddExperience(expAmount);
            }

            //捡到了就销毁经验球
            Destroy(gameObject);
        }
    }
}
}