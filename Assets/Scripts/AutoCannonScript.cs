using UnityEngine;
using UnityEngine.Animations;

#pragma warning disable CS0649

public class AutoCannonScript : MonoBehaviour
{
    private void Start()
    {
        animator = GetComponent<Animator>();
        aimConstraint = GetComponentInChildren<AimConstraint>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (disabled)
        {
            animator.enabled = false;
            return;
        }

        CannonView();
        RotateCannon();
    }

    private void RotateCannon()
    {
        if (animator.GetBool("Shooting"))
        {
            if (!aimConstraint.constraintActive)
            {
                aimConstraint.rotationAtRest =
                    new Vector3(0, 0, (float)(180 * startPoint.localRotation.z / 1.447780531062502));
                aimConstraint.constraintActive = true;
            }

            aimConstraint.weight += 0.04f;
        }
        else if (aimConstraint.constraintActive)
        {
            aimConstraint.rotationAtRest = new Vector3(0, 0, 60);
            aimConstraint.weight -= 0.04f;
            if (aimConstraint.weight <= 0)
                aimConstraint.constraintActive = false;
        }
    }

    private void CannonView()
    {
        Debug.DrawRay(shootPointLeft.position,
            shootPointLeft.right * viewDistance,
            Color.white);
        Debug.DrawRay(shootPointRight.position,
            shootPointRight.right * viewDistance,
            Color.white);

        const int rayCount = 40;
        var angle = fov / 2;
        var angleIncrease = fov / rayCount;

        for (var i = 0; i <= rayCount; i++)
        {
            var raycastHit2D = Physics2D.Raycast(startPoint.position,
                startPoint.rotation * GetVectorFromAngle(angle),
                viewDistance,
                layerMask);
            if (raycastHit2D && raycastHit2D.collider.CompareTag("Player"))
            {
                animator.SetBool("Shooting", true);

                var tempHit2D = Physics2D.Raycast(startPoint.position,
                    startPoint.rotation * GetVectorFromAngle(angle - angleIncrease * 3),
                    viewDistance,
                    layerMask);
                if (tempHit2D && tempHit2D.collider.CompareTag("Player"))
                    AimPoint.position = tempHit2D.point;
                else
                    AimPoint.position = raycastHit2D.point;


                break;
            }

            animator.SetBool("Shooting", false);
            angle -= angleIncrease;
        }
    }

    private static Vector3 GetVectorFromAngle(float angle)
    {
        var angleRag = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRag), Mathf.Sin(angleRag));
    }

    public void Shoot()
    {
        RayShoot(shootPointLeft);
        RayShoot(shootPointRight);
        audioSource.PlayOneShot(clip);
        var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
        soundInstance.GetComponent<VibrationCircle>().OnCreate(shootPointLeft.position, Color.yellow, 1, 45, 2);


        var soundInstanceà = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
        soundInstanceà.GetComponent<VibrationCircle>().OnCreate(shootPointRight.position, Color.yellow, 1, 45, 2);
    }

    private void RayShoot(Transform shootPoint)
    {
        var shoot = Physics2D.Raycast(shootPoint.position,
            shootPoint.right,
            viewDistance,
            layerMask);

        if (shoot)
        {
            var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
            soundInstance.GetComponent<VibrationCircle>().OnCreate(shoot.point, bulletHoleColor);
            if (shoot.collider.CompareTag("Player"))
                shoot.collider.GetComponentInParent<HealthSystem>().GetHit(300);
        }
    }

    #region Values

    private Animator animator;
    public float fov;
    public LayerMask layerMask;

    public Transform startPoint;
    public float viewDistance;
    public Color bulletHoleColor;
    [SerializeField] private Transform shootPointLeft;
    [SerializeField] private Transform shootPointRight;
    [SerializeField] private Transform AimPoint;
    private AimConstraint aimConstraint;
    private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    public bool disabled;

    #endregion
}