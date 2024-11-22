using UnityEngine;

class PlayerAnimator : MonoBehaviour {
  private Animator animator;
  private enum CrouchState {
    In,
    Idle,
    Out,
  }
  private static CrouchState crouchState = CrouchState.In;

  void Awake() {
    animator = GetComponent<Animator>();
  }

  void Update() {
    switch (PlayerStateManager.currentState)
    {
      case PlayerStateManager.PlayerState.Idle:
        animator.Play("Idle");
        if (crouchState.Equals(CrouchState.Out)) animator.Play("CrouchOut");
        break;

      case PlayerStateManager.PlayerState.Running:
        animator.Play("Running");
        break;

      case PlayerStateManager.PlayerState.Dashing:
        animator.Play("Dashing");
        break;

      case PlayerStateManager.PlayerState.Jumping:
        animator.Play("Jumping");
        break;

      case PlayerStateManager.PlayerState.Crouching:
        if (crouchState.Equals(CrouchState.In)) animator.Play("CrouchIn");
        if (crouchState.Equals(CrouchState.Idle)) animator.Play("CrouchIdle");
        break;
        
      default:
        break;
    }
  }

  public void OnCrouchInAnimationEnd() {
    crouchState = CrouchState.Idle;
  }
  public void OnCrouchOutAnimationEnd() {
    Debug.Log("Got here");
    crouchState = CrouchState.In;
  }
  public static void OnCrouchActionReleased() {
    crouchState = CrouchState.Out;
  }
}
