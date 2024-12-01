public interface IPlayerMovementState {
  void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager, PlayerMovementManager movementManager);
  void UpdateState();
  void ExitState();
}
