using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var flipFactor = animator.GetInteger("flipFactor");
        var origin = animator.transform.position + new Vector3(-flipFactor, 4);
        //Debug.DrawRay(origin,
        //    4 * flipFactor * animator.transform.right, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(origin,
            animator.transform.right * -flipFactor,
            2, 1 << 6);
        if (hit)
        {
            animator.SetTrigger("Stop");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Stop");
    }
}
