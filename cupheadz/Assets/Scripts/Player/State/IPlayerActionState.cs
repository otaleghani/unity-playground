public interface IPlayerActionState {
  void EnterState(PlayerStateManager stateManager, PlayerInputManager inputManager);
  void UpdateState();
  void ExitState();
}
