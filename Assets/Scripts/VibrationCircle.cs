using UnityEngine;

public class VibrationCircle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        transform.localScale += new Vector3(1, 1) / 600;

        var tmp = spriteRenderer.color;
        tmp.a -= (float)0.02;
        spriteRenderer.color = tmp;

        if (tmp.a <= 0)
            Destroy(transform.gameObject);
    }
}
