public class PlayerJumpingState : IPlayerMovementState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;
    // Add handlers
  }
  public void UpdateState() {
    if (PlayerMovementManager.isGrounded) {
      stateManager.ChangeMovementState(new PlayerIdleState());
    }
  }
  public void ExitState() {}
}
