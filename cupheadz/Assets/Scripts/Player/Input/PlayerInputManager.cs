using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputManager : MonoBehaviour {
  // Declare the different events that would be called on input performed
  public event Action<Vector2> OnMove;
  public event Action OnMoveCanceled;
  public event Action OnJump;
  public event Action OnJumpReleased;

  private PlayerInput playerInput;
  private InputAction moveAction;
  private InputAction jumpAction;

  void Awake() {
    playerInput = GetComponent<PlayerInput>();

    moveAction = playerInput.actions["Move"];
    jumpAction = playerInput.actions["Jump"];
  }

  void Update() {
    if (moveAction.ReadValue<Vector2>().x != 0) {
      OnMove?.Invoke(moveAction.ReadValue<Vector2>());
    } else {
      OnMoveCanceled?.Invoke();
    }
    if (jumpAction.ReadValue<float>() != 0) {
      OnJump?.Invoke();
    } else {
      OnJumpReleased?.Invoke();
    }
  }
}
