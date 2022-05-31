using System.Collections;
using UnityEngine;

public class SolderView : MonoBehaviour
{
    [SerializeField] private Transform aimPoint;


    private Animator animator;
    [SerializeField] private int Cooldown;
    private Coroutine coroutine;
    public float fov;
    public Vector2 lastKnownPoint;
    public LayerMask layerMask;
    public Transform[] LowerVentPoints;
    private int shootCooldown;
    public Transform startPoint;

    private int tempCooldown;
    private int timer;

    public Transform[] UpperVentPoints;

    private int ventNum;
    private Transform[] ventPoints;
    public float viewDistance;
    [SerializeField] private GameObject weapon;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //if (animator.GetBool("Death"))
        //{
        //    weapon.GetComponent<WeaponRifle>().isShoot = false;
        //    return;
        //}

        GoBackStopper();
        RayCastVision();
        AimFlip();
        ChangeMovementBehaviourMode();
        ShootingSystem();
    }

    private void GoBackStopper()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("MoveBack")) return;

        if (timer >= 5 * 50)
        {
            animator.SetTrigger("Stop");
            timer = 0;
        }

        timer++;
    }

    private void ShootingSystem()
    {
        if (shootCooldown > 0)
        {
            aimPoint.transform.position = lastKnownPoint;
            weapon.GetComponent<WeaponRifle>().isShoot = true;
        }
        else
        {
            weapon.GetComponent<WeaponRifle>().isShoot = false;
        }

        shootCooldown--;
    }

    private void ChangeMovementBehaviourMode()
    {
        if (tempCooldown <= 0)
        {
            var rnd = Random.value;
            if (!(rnd > 0.99)) return;
            animator.SetBool("ChangeMovement", true);
            tempCooldown = Cooldown * 50;
        }
        else
        {
            tempCooldown--;
        }
    }

    private void AimFlip()
    {
        if (!(aimPoint.localPosition.x < 3)) return;
        transform.position += new Vector3(animator.GetInteger("flipFactor") * 2, 0, 0);
        transform.localScale = new Vector3(-animator.transform.localScale.x, animator.transform.localScale.y,
            animator.transform.localScale.z);
        animator.SetInteger("flipFactor", -animator.GetInteger("flipFactor"));
        aimPoint.localPosition = new Vector3(-aimPoint.localPosition.x, aimPoint.localPosition.y);
        aimPoint.localPosition += new Vector3(2, 0, 0);
    }

    private void RayCastVision()
    {
        var rayCount = 40;
        var angle = fov / 2;
        var angleIncrease = fov / rayCount;


        for (var i = 0; i <= rayCount; i++)
        {
            var raycastHit2D = Physics2D.Raycast(startPoint.position,
                animator.GetInteger("flipFactor") * (startPoint.rotation *
                                                     Quaternion.Euler(0, 0, -70 * animator.GetInteger("flipFactor")) *
                                                     GetVectorFromAngle(angle)),
                viewDistance,
                layerMask);
            if (raycastHit2D && raycastHit2D.collider.CompareTag("Player"))
            {
                lastKnownPoint = raycastHit2D.point;
                if (animator.GetInteger("triggered") == 0)
                    animator.SetInteger("triggered", 1);
                shootCooldown = 80;
            }

            angle -= angleIncrease;
        }
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        var angleRag = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRag), Mathf.Sin(angleRag));
    }

    public void ChangeVentPoint()
    {
        if (weapon.GetComponent<WeaponRifle>().isShoot) return;
        ventPoints = transform.position.y + 3 < lastKnownPoint.y ? UpperVentPoints : LowerVentPoints;
        if (ventPoints.Length == 0) return;
        coroutine = StartCoroutine(SmoothAim(ventPoints[ventNum].position, aimPoint.position));
        ventNum++;
        if (ventNum >= ventPoints.Length) ventNum = 0;
    }

    private IEnumerator SmoothAim(Vector3 vent, Vector3 startPos)
    {
        while (Application.isPlaying)
        {
            if (aimPoint.position == vent)
                StopCoroutine(coroutine);
            aimPoint.position = Vector3.MoveTowards(aimPoint.position, vent, 1);

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Death()
    {
        animator.enabled = false;
    }

    public void DeathPrev()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}