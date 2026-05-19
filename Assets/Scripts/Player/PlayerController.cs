using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ÒÆ¶¯ËÙ¶È")]
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb;

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput = moveInput.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }


}
