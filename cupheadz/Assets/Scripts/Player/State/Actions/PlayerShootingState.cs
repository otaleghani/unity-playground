public class PlayerShootingState : IPlayerActionState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;

    inputManager.OnShootReleased += HandleShootingReleased;
  }
  public void UpdateState() {}
  public void ExitState() {
    inputManager.OnShootReleased -= HandleShootingReleased;
  }

  private void HandleShootingReleased() {
    stateManager.ChangeActionState(new PlayerNoneState());
  }
}