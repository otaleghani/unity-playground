public class PlayerMovingState : IPlayerMovementState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;
    // Add handlers

    inputManager.OnMoveCanceled += HandleMoveCanceled;
  }

  public void UpdateState() {
    if (!PlayerMovementManager.isGrounded) {
      stateManager.ChangeMovementState(new PlayerJumpingState());
    }
  }

  public void ExitState() {}

  public void HandleMoveCanceled() {
    stateManager.ChangeMovementState(new PlayerIdleState());
  }

  // Actions that you can do from the moving state
  public void HandleDash() {}
  public void HandleJump() {}
  // ...
}
