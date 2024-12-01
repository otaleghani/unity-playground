using UnityEngine;

public class PlayerStateManager : MonoBehaviour {
  private PlayerMovementManager movementManager;

  private PlayerInputManager inputManager;

  public IPlayerMovementState movementState;
  public IPlayerActionState actionState;

  void Awake() {
    inputManager = GetComponent<PlayerInputManager>();
    movementManager = GetComponent<PlayerMovementManager>();

    movementState = new PlayerIdleState();
    actionState = new PlayerNoneState();
  }

  void Start() {
    movementState.EnterState(this, inputManager, movementManager);
    actionState.EnterState(this, inputManager);
  }

  void FixedUpdate() {
    movementState.UpdateState();
    actionState.UpdateState();
  }

  public void ChangeMovementState(IPlayerMovementState newState) {
    movementState.ExitState();
    movementState = newState;
    movementState.EnterState(this, inputManager, movementManager);
  }

  public void ChangeActionState(IPlayerActionState newState) {
    actionState.ExitState();
    actionState = newState;
    actionState.EnterState(this, inputManager);
  }
}
