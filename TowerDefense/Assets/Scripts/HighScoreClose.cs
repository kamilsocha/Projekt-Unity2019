using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Disables highscores panel after end of animation.
/// Triggered by animation event.
/// </summary>
public class HighScoreClose : StateMachineBehaviour
{

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SetActive(false);
    }

}
