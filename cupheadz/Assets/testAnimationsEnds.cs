using UnityEngine;
using UnityEngine.Events;

public class testAnimationsEnds : StateMachineBehaviour
{
  public UnityEvent onStateExitEvent;

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (onStateExitEvent != null) onStateExitEvent.Invoke();
  }
}
