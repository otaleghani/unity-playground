using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementV3 : MonoBehaviour
{
  [Header("jump settings")]
  public float jumpForce = 1f;               // Initial jump force
  public float maxJumpTime = 0.3f;           // Maximum duration to apply sustained force
  public float sustainedJumpForce = 5f;     // Additional force while button is held


  // Variables to store input states
  private bool jumpPressed;
  private bool jumpHeld;
  private float jumpTimeCounter;

  private float movementSpeed = 5f;
  private float jumpSpeed = 5f;
  private float dashSpeed = 10f;
  private bool isFacingRight = true;
  private bool isGrounded = true;
  private bool isDashing = false;
  private bool isJumping = false;

  private Vector2 moveValue;
  private Animator animator;
  private Rigidbody2D rb;
  private PlayerInput playerInput;
  private AudioSource dashAudio;

  private InputAction moveAction;
  private InputAction shootAction;
  private InputAction jumpAction;
  private InputAction shootEXAction;
  private InputAction crouchAction;
  private InputAction switchWeaponAction; 
  private InputAction lockAction; 
  private InputAction dashAction; 


  private void Awake() {
    playerInput = GetComponent<PlayerInput>();
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    dashAudio = GetComponent<AudioSource>();

    moveAction = playerInput.actions["Move"];
    shootAction = playerInput.actions["Shoot"];
    jumpAction = playerInput.actions["Jump"];
    shootEXAction = playerInput.actions["ShootEX"];
    crouchAction = playerInput.actions["Crouch"];
    switchWeaponAction = playerInput.actions["SwitchWeapon"];
    lockAction = playerInput.actions["Lock"];
    dashAction = playerInput.actions["Dash"];

    moveAction.performed += OnMove;
    shootAction.performed += OnShoot;
    jumpAction.performed += OnJump;
    jumpAction.canceled += OnJumpCanceled;
    shootEXAction.performed += OnShootEX;
    crouchAction.performed += OnCrouch;
    crouchAction.canceled += OnCrouchCanceled;
    switchWeaponAction.performed += OnSwitchWeapon;
    lockAction.performed += OnLock;
    dashAction.performed += OnDash;

    moveAction.canceled += OnMoveCanceled;
  }


  public void OnDashFinishedEvent() {
    isDashing = false;
    animator.SetBool("isDashing", isDashing);
    //dashAudio.Stop();
  }

  public void OnMove(InputAction.CallbackContext context) {
    moveValue = context.ReadValue<Vector2>();
    if (isGrounded) animator.SetBool("isMoving", true);
  }
  public void OnMoveCanceled(InputAction.CallbackContext context) {
    moveValue = new Vector2(0,0);
    if (isGrounded) animator.SetBool("isMoving", false);
  }
  public void OnJump(InputAction.CallbackContext context) {
    jumpPressed = true;
    jumpHeld = true;
    //isGrounded = false;
    if (isGrounded) 
      animator.SetBool("isJumping", true);
  }
  public void OnJumpCanceled(InputAction.CallbackContext context) {
    jumpHeld = false;
  }

  public void OnShoot(InputAction.CallbackContext context) {
  }
  public void OnShootEX(InputAction.CallbackContext context) {
  }
  public void OnCrouch(InputAction.CallbackContext context) {
    animator.SetBool("isCrouchIn", true);
  }
  public void OnCrouchCanceled(InputAction.CallbackContext context) {
    //animator.SetBool("isCrouchIn", false);
    animator.SetBool("isCrouchIdle", false);
    animator.SetBool("isCrouchOut", true);
  }

  public void OnCrouchFinishedEvent() {
    animator.SetBool("isCrouchOut", false);
    animator.SetBool("isCrouchIn", false);
    animator.SetBool("isCrouchIdle", false);
  }
  public void OnCrouchIdle() {
    animator.SetBool("isCrouchIn", false);
    animator.SetBool("isCrouchIdle", true);
  }

  public void OnSwitchWeapon(InputAction.CallbackContext context) {
  }
  public void OnLock(InputAction.CallbackContext context) {
  }
  public void OnDash(InputAction.CallbackContext context) {
    isDashing = true;
    animator.SetBool("isDashing", isDashing);
    dashAudio.Play();
  }

  public void FixedUpdate() {
    Vector2 updatedPosition = new Vector2(rb.linearVelocityX, rb.linearVelocityY);

    if (isDashing) {
      updatedPosition.x = dashSpeed;
    } else {
      updatedPosition.x = moveValue.x * movementSpeed;
    }

    if (isGrounded && jumpPressed) {
      jumpTimeCounter = maxJumpTime;
      isJumping = true;
      jumpPressed = false;
      updatedPosition.y = jumpForce;
    }

    if (isJumping) {
      if (jumpHeld && jumpTimeCounter > 0) {
        updatedPosition.y = sustainedJumpForce;
        jumpTimeCounter -= Time.fixedDeltaTime;
      }
    } else {
      isJumping = false;
    }

    rb.linearVelocity = updatedPosition;
  }

  public void Update() {
    FlipCharacter();
  }


  void FlipCharacter() {
    if (isFacingRight && moveValue.x < 0f ||
    !isFacingRight && moveValue.x > 0f) {
      isFacingRight = !isFacingRight;
      Vector3 ls = transform.localScale;
      ls.x *= -1f;
      dashSpeed *= -1f;
      transform.localScale = ls;
    }
  }
  private void OnTriggerEnter2D(Collider2D collision) {
    isGrounded = true;
    animator.SetBool("isJumping", !isGrounded);
  }
}
