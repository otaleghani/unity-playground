public class PlayerJumpingState : IPlayerMovementState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;
    // Add handlers
  }
  public void UpdateState() {}
  public void ExitState() {}
}
