using UnityEngine;

public class PlayerStateManager : MonoBehaviour {
  public bool isGrounded = false;

  private PlayerInputManager inputManager;

  public IPlayerMovementState movementState;
  public IPlayerActionState actionState;

  void Awake() {
    inputManager = GetComponent<PlayerInputManager>();
    movementState = new PlayerIdleState();
  }

  void Start() {
    movementState.EnterState(this, inputManager);
    // I could do the same here for the movement
  }

  void Update() {
    movementState.UpdateState();
    Debug.Log(movementState);
  }

  public void ChangeMovementState(IPlayerMovementState newState) {
    movementState.ExitState();
    movementState = newState;
    movementState.EnterState(this, inputManager);
  }

  public void ChangeActionState(IPlayerActionState newState) {
    actionState.ExitState();
    actionState = newState;
    actionState.EnterState(this, inputManager);
  }
}
