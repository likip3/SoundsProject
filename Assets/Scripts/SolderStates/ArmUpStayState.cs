using UnityEngine;

public class ArmUpStayState : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MoveBack(animator);
    }

    private static void MoveBack(Animator animator)
    {
        var flipFactor = animator.GetInteger("flipFactor");
        var origin = animator.transform.position + new Vector3(-flipFactor, 4);
        var hit = Physics2D.Raycast(origin,
            animator.transform.right * -flipFactor,
            2, 1 << 6);


        if (!hit && Vector2.Distance(animator.GetComponent<SolderView>().lastKnownPoint, animator.rootPosition) < 10)
            animator.SetTrigger("GoBack");
    }
}