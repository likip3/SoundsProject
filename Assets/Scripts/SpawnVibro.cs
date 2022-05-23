using UnityEngine;

public class SpawnVibro : MonoBehaviour , IPooledObject
{
    public GameObject soundWave;
    public int lifePunches;
    private int tempLifePunches;
    [SerializeField] private SoundParticlePool.ObjectInfo.ObjectType waveObjectType;


    public void OnCollisionEnter2D(Collision2D col)
    {
        var soundInstance = SoundParticlePool.Instance.GetObject(waveObjectType);
        soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.white);

        tempLifePunches--;
        if (tempLifePunches <= 0)
        {
            SoundParticlePool.Instance.DestroyObject(gameObject);
        }
    }

    public SoundParticlePool.ObjectInfo.ObjectType Type => type;
    [SerializeField] 
    private SoundParticlePool.ObjectInfo.ObjectType type;

    public void OnCreate(Vector3 position, Quaternion rotation, float speed)
    {
        tempLifePunches = lifePunches;
        transform.position = position;
        transform.rotation = rotation;
        transform.GetComponent<Rigidbody2D>().AddForce(transform.right / 10 * speed);
    }
}
