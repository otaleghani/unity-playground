using UnityEngine;

public class PlayerLockState : IPlayerMovementState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;
  private PlayerMovementManager movementManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager, PlayerMovementManager movementManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;
    this.movementManager = movementManager;

    inputManager.OnLockReleased += HandleLockReleased;
  }

  public void UpdateState() {}

  public void ExitState() {
    inputManager.OnLockReleased -= HandleLockReleased;
  }

  private void HandleLockReleased() {
    stateManager.ChangeMovementState(new PlayerIdleState());
  }
}