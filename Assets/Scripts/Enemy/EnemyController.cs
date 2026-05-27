using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Enemy{
public class EnemyController : MonoBehaviour
{
    [Header("移动速度")]
    [SerializeField]private float moveSpeed = 2f;
    public float MoveSpeed {get => moveSpeed;set => moveSpeed = value;}

    //玩家对象的方位
    //静态的玩家引用
    private static Transform cachedPlayer;
    //非静态的玩家引用
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
        //缓存引用模式，若静态缓存还没初始化，才执行Find
        //第一个敌人会触发查找，之后的敌人都直接复用
        //找到玩家对象
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj !=null)
            {
                cachedPlayer = playerObj.transform;   //写入静态缓存
            }
            else
            {
                Debug.LogWarning("场景中找不到Player标签对象！", this);
                return;
            }
        //从静态缓存赋值给当前敌人的player字段
        player = cachedPlayer;

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
            //死亡重生后玩家可能被销毁了
            //那么就清除缓存，让下一个Start()重新查找
            cachedPlayer = null;
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