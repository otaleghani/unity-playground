using UnityEngine;

public class PlayerMovementManager : MonoBehaviour {
  [Header("General")]
  [SerializeField] private bool isFacingRight = true;
  public bool isGrounded = false;
  
  [Header("Movement proprieties")]
  [SerializeField] private float speed = 4f;
  [SerializeField] private float acceleration = 0f;
  [SerializeField] private float maxFallSpeed = -25f;

  [Header("Jump")]
  [SerializeField] private float jumpMaxHeight = 2.5f;
  [SerializeField] private float jumpMinHeight = 0.5f;
  [SerializeField] private float jumpTimeToMaxHeight = 0.4f;
  [SerializeField] private float fallGravityMultiplier = 2f;

  [SerializeField] private float jumpStateMinTime = 0.05f;
  [SerializeField] private float jumpStateMaxTime = 0.4f;
  [SerializeField] private float jumpForce = 5.0f;

  public bool isJumping;
  public bool jumpButtonReleased;
  private float jumpButtonPressedTime;
  private float initialJumpVelocity;
  private float gravity;
  private float jumpRateOfChange;

  //private float jumpTime;
  //private bool isJumpPressed = false;
  //private bool isJumpHeld = false;


  //private Vector2 moveValue;
  [Header("Dash")]
  public float dashSpeed = 10f;

  private Rigidbody2D rb;
  //private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;
  private PlayerStateManager stateManager;

  void Awake() {
    rb = GetComponent<Rigidbody2D>();
    inputManager = GetComponent<PlayerInputManager>();
    stateManager = GetComponent<PlayerStateManager>();
  }

  void Start() {
    inputManager.OnJump += HandleJump;
    inputManager.OnJumpReleased += HandleJumpReleased;
    inputManager.OnMove += HandleMove;
    inputManager.OnMoveCanceled += HandleMoveCanceled;
    inputManager.OnDash += HandleDash;
    inputManager.OnLock += HandleLock;
    inputManager.OnLockReleased += HandleLockReleased;
  }

  bool jumpActionHeld;
  float jumpTimeCounter;
  void HandleJump() {
    if (!isJumping && isGrounded) {
      jumpActionHeld = true;
      isJumping = true;
      isGrounded = false;
      jumpTimeCounter = 0f;
    }
  }

  void HandleJumpReleased() {
    jumpActionHeld = false;
  }

  void HandleMove(Vector2 mov) {
    acceleration = mov.x;
  }
  void HandleMoveCanceled() {
    acceleration = 0f;
  }

  private bool isLocked = false;
  void HandleLock() {
    isLocked = true;
  }
  void HandleLockReleased() {
    isLocked = false;
  }

  float dashCooldown;
  float dashMaxCooldown = 1f;
  public bool isDashingCooldown = false;
  public bool isDashing = false;
  void HandleDash() {
    isDashing = true;
  }

  // Counts how much the character was in the air
  public bool jumpReset;
  public void FixedUpdate() {
    if (!isLocked) {
      Vector2 updatedPosition = rb.linearVelocity;

      // handle jump
      if (isJumping && !jumpReset) {
        if (jumpTimeCounter <= jumpStateMinTime) {
          updatedPosition.y = jumpForce;
        }
        if (jumpTimeCounter <= jumpStateMaxTime && jumpActionHeld) {
          updatedPosition.y = jumpForce;
        } else {
          jumpReset = true;
          isJumping = false;
        }
        jumpTimeCounter += Time.fixedDeltaTime;
      } 

      // handle movement
      updatedPosition.x = speed * acceleration;

      // handle dashing
      if (isDashing && !isDashingCooldown) {
        updatedPosition.x = dashSpeed;
      } 

      if (isDashingCooldown) {
        dashCooldown -= Time.fixedDeltaTime;
      }
      if (dashCooldown <= 0) {
        isDashingCooldown = false;
      }

      rb.linearVelocity = updatedPosition;
    }
    FlipCharacter();
  }

  void FlipCharacter() {
    if (isFacingRight && acceleration < 0f ||
    !isFacingRight && acceleration > 0f) {
      isFacingRight = !isFacingRight;
      Vector3 ls = transform.localScale;
      ls.x *= -1f;
      dashSpeed *= -1f;
      transform.localScale = ls;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    isGrounded = true;
    isJumping = false;
    jumpReset = false;
  }

  public void OnDashingAnimationEnd() {
    isDashing = false;
    isDashingCooldown = true;
    dashCooldown = dashMaxCooldown;
    if (isGrounded) {
      stateManager.ChangeMovementState(new PlayerIdleState());
    } else {
      stateManager.ChangeMovementState(new PlayerJumpingState());
    }
  }
}