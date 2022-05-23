using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : StateMachineBehaviour
{
    public bool Shoot;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.DrawRay(((Transform)animator.GetComponentInChildren(typeof(Transform))).position,
        //    ((Transform)animator.GetComponentInChildren(typeof(Transform))).right * 100,
        //    Color.white);
        //if (Shoot) 
        //{

        //}
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
