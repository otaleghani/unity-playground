// Here the player is not shooting
public class PlayerNoneState : IPlayerActionState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;

    inputManager.OnShoot += HandleShooting;
  }
  public void UpdateState() {}
  public void ExitState() {
    inputManager.OnShoot -= HandleShooting;
  }

  private void HandleShooting() {
    stateManager.ChangeActionState(new PlayerShootingState());
  }
}
