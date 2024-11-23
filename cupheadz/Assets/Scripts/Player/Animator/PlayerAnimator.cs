using UnityEngine;

class PlayerAnimator : MonoBehaviour {
  private Animator animator;
  private PlayerStateManager playerStateManager;

  void Awake() {
    animator = GetComponent<Animator>();
    playerStateManager = GetComponent<PlayerStateManager>();
  }

  void Update() {
    if (playerStateManager.movementState is PlayerIdleState) {
      animator.Play("Idle");
    }
    if (playerStateManager.movementState is PlayerMovingState) {
      animator.Play("Running");
    }
    if (playerStateManager.movementState is PlayerJumpingState) {
      animator.Play("Jumping");
    }
  }
}
