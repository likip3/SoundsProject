using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRifle : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Color bulletHoleColor;

    private AudioSource audioSource;
    public AudioClip clip;
    public float cooldown;
    public bool isShoot;
    private Animator animator;

    private float tempCooldown;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
    }


    void FixedUpdate()
    {
        Debug.DrawRay(origin.position + new Vector3(0, (float)-0.06), Quaternion.Euler(0, 0, -1) * transform.right * 100 * animator.GetInteger("flipFactor"), Color.magenta);
        if (Cooldown() && isShoot)
        {
            tempCooldown = 50 * cooldown;
            RaycastHit2D hit = Physics2D.Raycast(origin.position + new Vector3(0, (float)-0.06),
                Quaternion.Euler(0, 0, -1) * transform.right * animator.GetInteger("flipFactor"),
                200, layerMask);
            audioSource.PlayOneShot(clip);
            if (hit)
            {
                var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
                soundInstance.GetComponent<VibrationCircle>().OnCreate(hit.point, bulletHoleColor);

                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponentInParent<HealthSystem>().GetHit(100);
                }
            }
        }
    }
    private bool Cooldown()
    {
        tempCooldown--;
        return tempCooldown <= 0;
    }
}
