using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enemy;
namespace Game.Player
{
public class PlayerController : MonoBehaviour
{
    [Header("玩家移动速度")]
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb;

    private Vector2 moveInput;

    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput = moveInput.normalized;

        //驱动动画
        if(animator != null)
        {
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            PlayerStats playerStats = GetComponent<PlayerStats>();
            if(playerStats !=null)
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                int damage = enemyHealth != null ? enemyHealth.contactDamage : 1;
                playerStats.TakeDamage(damage);
            }
        }
    }


}
}