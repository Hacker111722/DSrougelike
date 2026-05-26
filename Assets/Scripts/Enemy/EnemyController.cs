using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Enemy{
public class EnemyController : MonoBehaviour
{
    [Header("移动速度")]
    public float moveSpeed = 2f;

    //玩家对象的方位
    private Transform player;

    //rigidBody2D实例
    private Rigidbody2D rb;

    private Animator animator;

    //初始化刚体
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator  = GetComponent<Animator>();
    }



    // Start is called before the first frame update
    void Start()
    {
        //找到玩家对象
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //物理帧更新移动
    private void FixedUpdate()
    {
        MoveToPlayer();
    }


    private void MoveToPlayer()
    {
        //找不到玩家时返回
        if (player == null)
        {
            if(animator!= null) animator.SetInteger("State", 0);
            return;
        }
        //设定方向
        Vector2 direction = (player.position - transform.position).normalized;

        //设置移动速度
        rb.velocity = direction * moveSpeed;

        //驱动动画
        if(animator!=null) animator.SetInteger("State", 1);   //播放行走动画
    }
}
}