using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enemy;
namespace Game.Weapon{
public class Bullet : MonoBehaviour
{
    [Header("子弹的移动速度")]
    public float moveSpeed = 10f;

    [Header("子弹伤害")]
    public int damage = 1;

    [Header("子弹的存活时间")]
    public float lifeTime = 3f;

    private Vector2 moveDirection;

    //子弹初始化
    public void Init(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Start()
    {
        //子弹生存时间倒计时
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
    ///计算子弹的碰撞效果
    ///</summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //敌人受到攻击
        if (other.CompareTag("Enemy"))
        {
            //使敌人生命减少
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                //敌人受伤
                enemyHealth.TakeDamage(damage);
            }

            //碰撞后立马销毁子弹
            Destroy(gameObject); 
        }
    }

}
}
