using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //子弹控制器
    //1.子弹移动
    //2.命中敌人
    //3.自动销毁
    [Header("子弹速度")]
    public float moveSpeed = 10f;

    [Header("子弹伤害")]
    public int damage = 1;

    [Header("生存时间")]
    public float lifeTime = 3f;

    private Vector2 moveDirection;

    //初始化子弹方向
    public void Init(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Start()
    {
        //定时销毁
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        Move();
    }

    //子弹移动
    private void Move()
    {
        transform.Translate(moveDirection* moveSpeed*Time.deltaTime);
    }

    ///<summary>
    ///子弹移动
    ///</summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果碰到敌人
        if (other.CompareTag("Enemy"))
        {
            //获取敌人生命组件
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                //造成伤害
                enemyHealth.TakeDamage(damage);
            }

            //销毁子弹
            Destroy(gameObject); 
        }
    }

}
