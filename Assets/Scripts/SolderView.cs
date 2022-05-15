using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderView : MonoBehaviour
{
    public Transform startPoint;
    public LayerMask layerMask;
    public float fov;
    public float viewDistance;
    public Vector2 lastKnownPoint;
    [SerializeField] private Transform aimPoint;

    private int tempCooldown;
    [SerializeField] private int Cooldown;


    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RayCastVision();
        AimFlip();
        ChangeMovementBehaviourMode();
    }

    private void ChangeMovementBehaviourMode()
    {
        if (tempCooldown <= 0)
        {
            var rnd = UnityEngine.Random.value;
            if (!(rnd > 0.99)) return;
            animator.SetBool("ChangeMovement", true);
            tempCooldown = Cooldown * 50;
        } else
        {
            tempCooldown--;
        }
    }

    private void AimFlip()
    {
        if (!(aimPoint.localPosition.x < 3)) return;
        animator.transform.position += new Vector3(animator.GetInteger("flipFactor") * 2, 0, 0);
        animator.transform.localScale = new Vector3(-animator.transform.localScale.x, animator.transform.localScale.y, animator.transform.localScale.z);
        animator.SetInteger("flipFactor", -animator.GetInteger("flipFactor"));
    }

    private void RayCastVision()
    {
        int rayCount = 20;
        float angle = fov / 2;
        float angleIncrease = fov / rayCount;


        for (var i = 0; i <= rayCount; i++)
        {
            //Debug.DrawRay(startPoint.position,
            //    animator.GetInteger("flipFactor") * viewDistance * GetVectorFromAngle(angle),
            //    Color.cyan);

            RaycastHit2D raycastHit2D = Physics2D.Raycast(startPoint.position,
                GetVectorFromAngle(angle) * animator.GetInteger("flipFactor"),
                viewDistance,
                layerMask);
            if (raycastHit2D && raycastHit2D.collider.CompareTag("Player"))
            {
                lastKnownPoint = raycastHit2D.point;
                if (animator.GetInteger("triggered") == 0)
                    animator.SetInteger("triggered", 1);
            }
            angle -= angleIncrease;
        }
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        var angleRag = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRag), Mathf.Sin(angleRag));
    }
}
