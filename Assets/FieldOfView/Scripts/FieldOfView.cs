
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    private float fov;
    private float viewDistance;
    private Vector3 origin;
    private float startingAngle;

    private void Start()
    {
        fov = 90f;
        viewDistance = 50f;
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;


        for (int i = 0; i <= rayCount; i++)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {

            }

            angle -= angleIncrease;
        }



    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        var angleRag = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRag), Mathf.Sign(angleRag));
    }
}