using UnityEngine;

class PlayerAnimator : MonoBehaviour {
  private Animator animator;
  private PlayerStateManager playerStateManager;
  private PlayerInputManager inputManager;

  void Awake() {
    animator = GetComponent<Animator>();
    playerStateManager = GetComponent<PlayerStateManager>();
    inputManager = GetComponent<PlayerInputManager>();
  }

  void Start() {
    inputManager.OnMove += HandleOnMove;
    inputManager.OnMoveCanceled += HandleOnMoveReleased;
    inputManager.OnMoveY += HandleOnMoveY;
    inputManager.OnMoveYCanceled += HandleOnMoveYReleased;
  }

  private float currentX = 0; 
  private float currentY = 0; 
  private void HandleOnMove(Vector2 vec) {
    currentX = vec.x;
  }
  private void HandleOnMoveY(Vector2 vec) {
    currentY = vec.y;
  }
  private void HandleOnMoveReleased() {
    currentX = 0;
  }
  private void HandleOnMoveYReleased() {
    currentY = 0;
  }

  void FixedUpdate() {
    if (playerStateManager.movementState is PlayerIdleState) {
      animator.Play("Idle");
    }
    if (playerStateManager.movementState is PlayerMovingState) {
      if (playerStateManager.actionState is PlayerShootingState) {
        animator.Play("ShootingMovement");
      } else {
        animator.Play("Running");
      }
    }
    if (playerStateManager.movementState is PlayerJumpingState) {
      animator.Play("Jumping");
    }
    if (playerStateManager.movementState is PlayerDashingState) {
      animator.Play("Dashing");
    }
    if (playerStateManager.movementState is PlayerLockState) {
      if (playerStateManager.actionState is PlayerShootingState) {
        if (currentX == 0 && currentY == 0) {
          animator.Play("ShootingOn");
        }
        if (currentX == 0) {
          if (currentY > 0) {
            animator.Play("ShootingOnUp");
          } else if (currentY < 0) {
            animator.Play("ShootingOnDown");
          }
        }
        if (currentX != 0) {
          if (currentY > 0) {
            animator.Play("ShootingOnObliqueUp");
          } else if (currentY < 0) {
            animator.Play("ShootingOnObliqueDown");
          }
        }
      }
      if (playerStateManager.actionState is PlayerNoneState) {
        if (currentX == 0 && currentY == 0) {
          animator.Play("ShootingOff");
        }
        if (currentX == 0) {
          if (currentY > 0) {
            animator.Play("ShootingOffUp");
          } else if (currentY < 0) {
            animator.Play("ShootingOffDown");
          }
        }
        if (currentX != 0) {
          if (currentY > 0) {
            animator.Play("ShootingOffObliqueUp");
          } else if (currentY < 0) {
            animator.Play("ShootingOffObliqueDown");
          }
        }
      }
    }
  }
}
