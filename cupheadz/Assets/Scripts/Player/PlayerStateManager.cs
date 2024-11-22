using UnityEngine;
using UnityEngine.InputSystem;

class PlayerStateManager : MonoBehaviour {
  public enum PlayerState {
    Idle,
    Locked,
    Running,
    Jumping,
    Dashing,
    JumpingAndDashing,  // Don't know if I actually need it
    Crouching,
    CrouchingIn,
    CrouchingOut,       // This is the one. 
    Shooting,
    ShootingEX,
    ShootingAndMoving,
    ShootingAndCrouching,
    ShootingAndLocked,
    TakingDamage,
    Dead,
  }

  private PlayerInput playerInput;
  private InputAction moveAction;
  private InputAction shootAction;
  private InputAction jumpAction;
  private InputAction shootEXAction;
  private InputAction crouchAction;
  private InputAction switchWeaponAction; 
  private InputAction lockAction; 
  private InputAction dashAction; 
  private PlayerMovement playerMovement;

  public static PlayerState currentState = PlayerState.Idle;
  
  private void Awake() {
    playerInput = GetComponent<PlayerInput>();
    playerMovement = GetComponent<PlayerMovement>();

    moveAction = playerInput.actions["Move"];
    shootAction = playerInput.actions["Shoot"];
    jumpAction = playerInput.actions["Jump"];
    shootEXAction = playerInput.actions["ShootEX"];
    crouchAction = playerInput.actions["Crouch"];
    lockAction = playerInput.actions["Lock"];
    dashAction = playerInput.actions["Dash"];
    //switchWeaponAction = playerInput.actions["SwitchWeapon"];
    //currentState
  }

  void Update() {
    Debug.Log(currentState);
    switch (currentState) {
      case PlayerState.Idle:
        //if (shootAction.WasPerformedThisFrame()) {
        //  if (crouchAction.WasPerformedThisFrame()) ChangePlayerState(PlayerState.ShootingAndCrouching);
        //  if (lockAction.WasPerformedThisFrame()) ChangePlayerState(PlayerState.ShootingAndLocked);
        //  if (moveAction.WasPerformedThisFrame()) ChangePlayerState(PlayerState.ShootingAndMoving);
        //} else ChangePlayerState(PlayerState.Shooting);

        if (moveAction.WasPerformedThisFrame()) currentState = PlayerState.Running;
        if (jumpAction.WasPerformedThisFrame()) currentState = PlayerState.Jumping;
        if (dashAction.WasPerformedThisFrame()) currentState = PlayerState.Dashing;
        if (crouchAction.WasPerformedThisFrame()) currentState = PlayerState.Crouching;
        //if (lockAction.WasPerformedThisFrame()) ChangePlayerState(PlayerState.Locked);
        //if (shootEXAction.WasPerformedThisFrame()) ChangePlayerState(PlayerState.ShootingEX);
        // I will have something like "PlayerCollisionManager.TookDamageThisFrame()" that would 
        // handle this state.
        break;

      case PlayerState.Jumping:
        // If you are jumping you cannot do anything asid of dashing
        if (dashAction.WasPerformedThisFrame()) currentState = PlayerState.Dashing;
        break;

      case PlayerState.Dashing:
        // If I'm dashing, I want to check If I'm airborne or not
        // if (playerMovement.isGrounded) currentState = PlayerState.Idle; else currentState = PlayerState.Jumping;
        break;

      case PlayerState.Running: 
        if (moveAction.WasReleasedThisFrame()) currentState = PlayerState.Idle;
        break;

      case PlayerState.Crouching: 
        if (crouchAction.WasReleasedThisFrame()) {
          currentState = PlayerState.Idle;
          PlayerAnimator.OnCrouchActionReleased();
        }
        break;
    }
  }

  public void OnDashingAnimationEnd() {
    if (playerMovement.isGrounded) {
      currentState = PlayerState.Idle;
    } else currentState = PlayerState.Jumping;
  }
}
