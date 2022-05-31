using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private float fov;

    [SerializeField] private LayerMask layerMask;
    private Vector3 origin;
    private float startingAngle;
    private float viewDistance;

    private void Start()
    {
        fov = 90f;
        viewDistance = 50f;
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        var rayCount = 50;
        var angle = startingAngle;
        var angleIncrease = fov / rayCount;


        for (var i = 0; i <= rayCount; i++)
        {
            var raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
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