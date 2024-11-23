public interface IPlayerMovementState {
  void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager);
  void UpdateState();
  void ExitState();
}
