using UnityEngine;

public class StayState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.applyRootMotion = false;
        animator.SetBool("ChangeMovement", false);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.applyRootMotion = true;
        //animator.rootPosition = animator.transform.position;
    }
}
