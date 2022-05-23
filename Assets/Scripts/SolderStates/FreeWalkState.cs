using UnityEngine;

public class FreeWalkState : StateMachineBehaviour
{
    public int flipFactor;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetBool("ChangeMovement", false);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        flipFactor = animator.GetInteger("flipFactor");
        var origin = animator.transform.position + new Vector3(flipFactor, 4);
        //Debug.DrawRay(origin,
        //    4 * flipFactor * animator.transform.right, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(origin,
            animator.transform.right * flipFactor,
            4, 1 << 6);
        if (hit)
        {
            animator.transform.localScale = new Vector3(-animator.transform.localScale.x, animator.transform.localScale.y, animator.transform.localScale.z);
            animator.SetInteger("flipFactor", -animator.GetInteger("flipFactor"));
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetLayerWeight(1, 0);
    }
}
