using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementV2 : MonoBehaviour
{
    float characterSpeed = 5f;
    float characterJumpPower = 5f;
    float characterDashSpeed = 200f;
    bool isFacingRight = true;
    bool isGrounded = false;
    bool isTakingDamage = false;
    bool isMoving = false;
    InputAction moveAction;
    InputAction jumpAction;
    Animator animator;
    Vector2 moveValue;

    private Rigidbody2D rb;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();

        if (moveValue.x != 0f && isGrounded) {
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
        } else {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
        }

        FlipCharacter();
        if (jumpAction.triggered && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocityX,
                characterJumpPower
            );
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }

        if (jumpAction.triggered && !isGrounded)
        {
            rb.linearVelocity = new Vector2(
                characterDashSpeed,
                rb.linearVelocityY
            );
            animator.SetBool("isDashing", true);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            moveValue.x * characterSpeed,
            rb.linearVelocityY
        );
    }

    void FlipCharacter() {
        if (isFacingRight && moveValue.x < 0f ||
        !isFacingRight && moveValue.x > 0f) {
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

    public void OnAnimationEnd()
    {
        Debug.Log("Animation finished");
    }
}
