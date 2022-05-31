using UnityEngine;

public class VibrationCircle : MonoBehaviour, IPooledObject
{
    private float alphaSpeed;

    private float increaseScaleSpeed;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private SoundParticlePool.ObjectInfo.ObjectType type;

    public SoundParticlePool.ObjectInfo.ObjectType Type => type;

    private void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.localScale += new Vector3(increaseScaleSpeed, increaseScaleSpeed) / 600;

        var tmp = spriteRenderer.color;
        tmp.a -= 0.02f * alphaSpeed;
        spriteRenderer.color = tmp;

        if (tmp.a <= 0)
            SoundParticlePool.Instance.DestroyObject(gameObject);
    }

    public void OnCreate(Vector3 position, Color color)
    {
        OnCreate(position, color, 1, 1, 1);
    }

    public void OnCreate(Vector3 position, Color color, float startScale, float scaleSpeed, float fadeSpeed)
    {
        transform.position = position;
        GetComponent<SpriteRenderer>().color = color;
        transform.localScale = new Vector3(startScale * 0.097f, startScale * 0.097f);
        increaseScaleSpeed = scaleSpeed;
        alphaSpeed = fadeSpeed;
    }
}