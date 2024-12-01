public class PlayerDashingState : IPlayerMovementState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;
  private PlayerMovementManager movementManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager, PlayerMovementManager movementManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;
    this.movementManager = movementManager;
  }

  public void UpdateState() {
    if (!movementManager.isDashing) {
      stateManager.ChangeMovementState(new PlayerIdleState());
    }
  }

  public void ExitState() {
  }
}
