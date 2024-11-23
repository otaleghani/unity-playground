using UnityEngine;

public class PlayerMovementManager : MonoBehaviour {
  [Header("General")]
  [SerializeField] private bool isFacingRight = true;
  public static bool isGrounded = false;
  
  [Header("Movement proprieties")]
  [SerializeField] private float speed = 4f;
  [SerializeField] private float acceleration = 1f;
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
  //[Header("Dash")]
  //public float dashSpeed = 10f;

  private Rigidbody2D rb;
  //private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;

  void Awake() {
    rb = GetComponent<Rigidbody2D>();
    inputManager = GetComponent<PlayerInputManager>();
  }

  void Start() {
    inputManager.OnJump += HandleJump;
    inputManager.OnJumpReleased += HandleJumpReleased;
  }

  bool jumpActionHeld;
  float jumpTimeCounter;
  void HandleJump() {
    Debug.Log("SUS");
    if (!isJumping && isGrounded) {
      Debug.Log("SAS");
      jumpActionHeld = true;
      isJumping = true;
      isGrounded = false;
      jumpTimeCounter = 0f;
    }
  }

  void HandleJumpReleased() {
    jumpActionHeld = false;
  }

  // Counts how much the character was in the air
  public bool jumpReset;
  public void FixedUpdate() {
    Vector2 updatedPosition = rb.linearVelocity;


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

    rb.linearVelocity = updatedPosition;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    Debug.Log("HERE");
    isGrounded = true;
    isJumping = false;
    jumpReset = false;
  }
}
