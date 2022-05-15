using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRifle : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private GameObject soundWave;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Color bulletHoleColor;
    private AudioSource audioSource;
    public AudioClip clip;
    public float cooldown;

    private float tempCooldown;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        Debug.DrawRay(origin.position + new Vector3(0, (float)-0.06), Quaternion.Euler(0, 0, -1) * transform.right * 100, Color.magenta);
        if (Cooldown())
        {
            tempCooldown = 50 * cooldown;
            RaycastHit2D hit = Physics2D.Raycast(origin.position + new Vector3(0, (float)-0.06),
                Quaternion.Euler(0, 0, -1) * transform.right,
                200, layerMask);
            audioSource.PlayOneShot(clip);
            if (hit)
            {
                var par = Instantiate(soundWave, hit.point, new Quaternion());
                par.GetComponent<VibrationCircle>().color = bulletHoleColor;
                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponentInParent<HealthSystem>().GetHit(10, new Vector2());
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
