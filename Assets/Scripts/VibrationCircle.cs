using UnityEngine;

public class VibrationCircle : MonoBehaviour , IPooledObject
{
    private SpriteRenderer spriteRenderer;

    public SoundParticlePool.ObjectInfo.ObjectType Type => type;
    [SerializeField]
    private SoundParticlePool.ObjectInfo.ObjectType type;

    private void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.localScale += new Vector3(1, 1) / 600;

        var tmp = spriteRenderer.color;
        tmp.a -= (float)0.02;
        spriteRenderer.color = tmp;

        if (tmp.a <= 0)
            SoundParticlePool.Instance.DestroyObject(gameObject);
    }

    public void OnCreate(Vector3 position, Color color)
    {
        OnCreate(position, color,1);
    }
    public void OnCreate(Vector3 position, Color color,float startScale)
    {
        transform.position = position;
        GetComponent<SpriteRenderer>().color = color;
        transform.localScale = new Vector3(startScale * 0.097f, startScale * 0.097f);
    }


}
