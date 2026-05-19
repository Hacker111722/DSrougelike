using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("敌人移动速度")]
    public float moveSpeed = 2f;

    //玩家目标
    private Transform player;

    //rigidBody2D组件
    private Rigidbody2D rb;

    //初始化
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    // Start is called before the first frame update
    void Start()
    {
        //查找玩家对象
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //固定帧更新物理移动
    private void FixedUpdate()
    {
        MoveToPlayer();
    }


    private void MoveToPlayer()
    {
        //如果玩家不存在
        if (player == null)
        {
            return;
        }
        //计算方向
        Vector2 direction = (player.position - transform.position).normalized;

        //设置速度
        rb.velocity = direction * moveSpeed;
    }
}
