using UnityEngine;

public class VibrationCircle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color color;
    void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
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
