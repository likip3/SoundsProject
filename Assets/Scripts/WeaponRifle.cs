using UnityEngine;

public class WeaponRifle : MonoBehaviour
{
    private Animator animator;

    private AudioSource audioSource;
    [SerializeField] private Color bulletHoleColor;
    public AudioClip clip;
    public float cooldown;
    public bool isShoot;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform origin;
    [SerializeField] private Transform soundPoint;

    private float tempCooldown;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
    }


    private void FixedUpdate()
    {
        //Debug.DrawRay(origin.position + new Vector3(0, -0.06f),
        //    Quaternion.Euler(0, 0, -1) * transform.right * 50 * animator.GetInteger("flipFactor"), Color.magenta);
        if (Cooldown() && isShoot)
        {
            tempCooldown = 50 * cooldown;
            var hit = Physics2D.Raycast(origin.position + new Vector3(0, -0.06f),
                Quaternion.Euler(0, 0, -1) * transform.right * animator.GetInteger("flipFactor"),
                200, layerMask);
            audioSource.PlayOneShot(clip);
            var soundInstanceBoom =
                SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
            soundInstanceBoom.GetComponent<VibrationCircle>().OnCreate(soundPoint.position, Color.yellow, 0.01f, 10, 1);
            if (hit)
            {
                var soundInstance =
                    SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
                soundInstance.GetComponent<VibrationCircle>().OnCreate(hit.point, bulletHoleColor);

                if (hit.collider.CompareTag("Player")) hit.collider.GetComponentInParent<HealthSystem>().GetHit(100);
            }
        }
    }

    private bool Cooldown()
    {
        tempCooldown--;
        return tempCooldown <= 0;
    }
}