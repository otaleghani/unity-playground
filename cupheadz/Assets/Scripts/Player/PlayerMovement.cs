using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
  [Header("Jump")]
  public float jumpForce = 1f;
  public float jumpMaxTime = 0.3f;
  public float jumptSustainedForce = 5f;

  private float jumpTime;
  private bool isJumpPressed = false;
  private bool isJumpHeld = false;
  public bool isGrounded = true;

  private bool isFacingRight = true;

  
  [Header("Movement")]
  public float movementSpeed = 5f;

  [Header("Dash")]
  public float dashSpeed = 10f;

  private Rigidbody2D rb;
  private Vector2 moveValue;
  private PlayerStateManager playerStateManager;

  void Awake() {
    rb = GetComponent<Rigidbody2D>();
    playerStateManager = GetComponent<PlayerStateManager>();
  }

  public void Move(Vector2 value) {
    moveValue = value;
  }


  public void FixedUpdate() {
    Vector2 updatedPosition = rb.linearVelocity;
    updatedPosition.x = moveValue.x * movementSpeed;


    //if (isDashing) {
    //  updatedPosition.x = dashSpeed;
    //} else {
    //  updatedPosition.x = moveValue.x * movementSpeed;
    //}
    rb.linearVelocity = updatedPosition;
  }
}
