using UnityEngine;

public class PlayerMovementManager : MonoBehaviour {
  [Header("General")]
  [SerializeField] private bool isFacingRight = true;
  public bool isGrounded = false;
  
  [Header("Movement proprieties")]
  [SerializeField] private float speed = 4f;
  [SerializeField] private float acceleration = 1f;
  [SerializeField] private float maxFallSpeed = -25f;

  [Header("Jump")]
  [SerializeField] private float jumpMaxHeight = 2.5f;
  [SerializeField] private float jumpMinHeight = 0.5f;
  [SerializeField] private float jumpTimeToMaxHeight = 0.4f;
  [SerializeField] private float jumpStateMinTime = 0.05f;
  [SerializeField] private float fallGravityMultiplier = 2f;
  private bool isJumping;
  private bool jumpButtonReleased;
  private float jumpButtonPressedTime;
  private float initialJumpVelocity;
  private float gravity;
  private float jumpRateOfChange;

  //private float jumpTime;
  //private bool isJumpPressed = false;
  //private bool isJumpHeld = false;


  //private Vector2 moveValue;
  //[Header("Dash")]
  //public float dashSpeed = 10f;

  private Rigidbody2D rb;
  //private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;

  void Awake() {
    rb = GetComponent<Rigidbody2D>();
    //stateManager = GetComponent<PlayerStateManager>();
    inputManager = GetComponent<PlayerInputManager>();

    //gravity = -(2 * jumpMaxHeight) / Mathf.Pow(jumpTimeToMaxHeight, 2);
    //rb.gravityScale = gravity / Physics2D.gravity.y;
    //initialJumpVelocity = Mathf.Abs(gravity) * jumpTimeToMaxHeight;
  }

  void Start() {
    inputManager.OnJump += HandleJump;
    inputManager.OnJumpReleased += HandleJumpReleased;
  }

  bool jumpPressed;
  void HandleJump() {
    jumpPressed = true;
  }

  void HandleJumpReleased() {
    jumpButtonReleased = true;
  }

  // Counts how much the character was in the air
  float jumpTimeCounter;
  public void FixedUpdate() {
    Vector2 updatedPosition = rb.linearVelocity;

    if (isGrounded && jumpPressed) {
      jumpTimeCounter = jumpTimeToMaxHeight;
      isJumping = true;
      jumpPressed = false;
      updatedPosition.y = jumpRateOfChange;
    }
    if (isJumping) {
      if (jumpTimeCounter > 0) {
        //updatedPosition.y = sustainedJumpForce;
        jumpTimeCounter -= Time.fixedDeltaTime;
      } else {
        isJumping = false;
      }
    }

    rb.linearVelocity = updatedPosition;
  }

}
