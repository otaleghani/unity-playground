using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 8f;
    bool isFacingRight = true;
    float jumpPower = 4f;
    bool isGrounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       horizontalInput = Input.GetAxis("Horizontal");
       FlipSprite();
       if(Input.GetButtonDown("Jump") && isGrounded) 
       {
            rb.linearVelocity = new Vector2(
                rb.linearVelocityX, jumpPower
            );
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
       }
    }

    private void FixedUpdate() 
    {
        rb.linearVelocity = new Vector2(
            horizontalInput *
            moveSpeed,
            rb.linearVelocityY
        );
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocityX));
        animator.SetFloat("yVelocity", rb.linearVelocityY);
    }

    void FlipSprite()
    {
        if(isFacingRight && horizontalInput < 0f ||
        !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }
}
