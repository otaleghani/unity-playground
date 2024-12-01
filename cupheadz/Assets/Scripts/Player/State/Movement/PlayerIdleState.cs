using UnityEngine;

public class PlayerIdleState : IPlayerMovementState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;
  private PlayerMovementManager movementManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager, PlayerMovementManager movementManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;
    this.movementManager = movementManager;
    inputManager.OnMove += HandleMove;
    inputManager.OnJump += HandleJump;
    inputManager.OnDash += HandleDash;
    inputManager.OnLock += HandleLock;

    movementManager.isDashing = false;
  }

  // Do I need the UpdateState?
  public void UpdateState() {
    if (!movementManager.isGrounded) {
      stateManager.ChangeMovementState(new PlayerJumpingState());
    }
  }

  public void ExitState() {
    inputManager.OnMove -= HandleMove;
    inputManager.OnJump -= HandleJump;
    inputManager.OnDash -= HandleDash;
  }

  private void HandleJump() {
    stateManager.ChangeMovementState(new PlayerJumpingState());
  }

  private void HandleMove(Vector2 movement) {
    stateManager.ChangeMovementState(new PlayerMovingState());
  }

  private void HandleDash() {
    if (!movementManager.isDashingCooldown) {
      stateManager.ChangeMovementState(new PlayerDashingState());
    }
  }

  private void HandleLock() {
    stateManager.ChangeMovementState(new PlayerLockState());
  }
}