using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmUpStayState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<SolderView>().shootCooldown > 50)
        {

        }
        //Debug.Log(Vector2.Distance(animator.GetComponent<SolderView>().lastKnownPoint, animator.rootPosition));
        MoveBack(animator);
    }

    private static void MoveBack(Animator animator)
    {
        if (Vector2.Distance(animator.GetComponent<SolderView>().lastKnownPoint, animator.rootPosition) < 20)
        {
            animator.SetTrigger("GoBack");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
