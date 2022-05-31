using UnityEngine;

public class SpawnVibro : MonoBehaviour, IPooledObject
{
    public float damage;
    public int lifePunches;
    public GameObject soundWave;
    private int tempLifePunches;
    private int lifeTimer = 50 * 6;

    [SerializeField] private SoundParticlePool.ObjectInfo.ObjectType type;

    public SoundParticlePool.ObjectInfo.ObjectType Type => type;


    public void OnCollisionEnter2D(Collision2D col)
    {
        var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
        soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.white);
        if (col.transform.CompareTag("Enemy")) col.gameObject.GetComponent<EnemyHealth>().GetHit(damage);
        tempLifePunches--;
        if (tempLifePunches <= 0) SoundParticlePool.Instance.DestroyObject(gameObject);
        lifeTimer = 50 * 6;
    }

    private void FixedUpdate()
    {
        lifeTimer--;
        if (lifeTimer <= 0) SoundParticlePool.Instance.DestroyObject(gameObject);
    }

    public void OnCreate(Vector3 position, Quaternion rotation, float speed, float _damage, int lifeCollide)
    {
        damage = _damage;
        lifePunches = lifeCollide;
        tempLifePunches = lifePunches;
        transform.position = position;
        transform.rotation = rotation;
        transform.GetComponent<Rigidbody2D>().AddForce(transform.right / 10 * speed);
    }
}