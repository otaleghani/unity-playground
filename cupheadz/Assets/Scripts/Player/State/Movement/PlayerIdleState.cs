using UnityEngine;

public class PlayerIdleState : IPlayerMovementState {
  private PlayerStateManager stateManager;
  private PlayerInputManager inputManager;

  public void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager) {
    this.stateManager = stateManager;
    this.inputManager = inputManager;
    inputManager.OnMove += HandleMove;
    //inputManager.OnJump += HandleJump;
  }

  // Do I need the UpdateState?
  public void UpdateState() {
    if (!stateManager.isGrounded) {
      // You are jumping / falling!!
      // stateManager.ChangeMovementState(new PlayerJumpingState());
    }
  }

  public void ExitState() {
    inputManager.OnMove -= HandleMove;
  }

  private void HandleMove(Vector2 movemenet) {
    stateManager.ChangeMovementState(new PlayerMovingState());
  }

  //private void HandleJump() {
  //  stateManager.ChangeMovementState(new PlayerJumpingState());
  //}
}
