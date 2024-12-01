public class PlayerJumpingState : IPlayerMovementState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;
  private PlayerMovementManager movementManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager, PlayerMovementManager movementManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;
    this.movementManager = movementManager;
    // Add handlers
  }
  public void UpdateState() {
    if (movementManager.isGrounded) {
      stateManager.ChangeMovementState(new PlayerIdleState());
    }
  }

  // handle dash

  public void ExitState() {}
}
