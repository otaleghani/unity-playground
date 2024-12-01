public class PlayerMovingState : IPlayerMovementState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;
  private PlayerMovementManager movementManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager, PlayerMovementManager movementManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;
    this.movementManager = movementManager;
    // Add handlers

    inputManager.OnMoveCanceled += HandleMoveCanceled;
    inputManager.OnJump += HandleJump;
    inputManager.OnLock += HandleLock;
  }

  public void UpdateState() {
    if (!movementManager.isGrounded) {
      stateManager.ChangeMovementState(new PlayerJumpingState());
    }
  }

  public void ExitState() {
    inputManager.OnJump -= HandleJump;
    inputManager.OnLock -= HandleJump;
    inputManager.OnMoveCanceled -= HandleJump;
  }

  public void HandleMoveCanceled() {
    stateManager.ChangeMovementState(new PlayerIdleState());
  }

  // Actions that you can do from the moving state
  public void HandleDash() {}
  public void HandleJump() {
    stateManager.ChangeMovementState(new PlayerJumpingState());
  }
  private void HandleLock() {
    stateManager.ChangeMovementState(new PlayerLockState());
  }
}