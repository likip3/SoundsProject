using UnityEngine;

// ReSharper disable UnusedMember.Local

public class SoundsSystem : MonoBehaviour
{
    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            var spawnPos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - createPoint.position;
            var angle = attackAngel / 2f;
            float angleIncrease = attackAngel / particleCount;
            for (var i = 0; i <= particleCount; i++)
            {
                var soundInstance = SoundParticlePool.Instance.GetObject(parObjectType);
                soundInstance.GetComponent<SpawnVibro>().OnCreate(createPoint.position,
                    Quaternion.Euler(0, 0, Mathf.Atan2(spawnPos.y, spawnPos.x) * Mathf.Rad2Deg + angle), particleSpeed,
                    damage, lifePunches);
                angle -= angleIncrease;
            }

            tempCooldown = cooldown * 50;
            canShoot = false;
        }
    }

    private void FixedUpdate()
    {
        Cooldown();
    }

    private void Cooldown()
    {
        if (canShoot) return;
        tempCooldown--;
        if (tempCooldown <= 0) canShoot = true;
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        var angleRag = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRag), Mathf.Sign(angleRag));
    }

    #region Main

    public GameObject soundParticle;
    public Transform createPoint;
    public Camera mainCamera;
    public int particleCount;
    public int attackAngel;
    public float cooldown;
    public float particleSpeed;
    public float damage;
    public int lifePunches;

    private bool canShoot = true;
    private float tempCooldown;

    [SerializeField] private SoundParticlePool.ObjectInfo.ObjectType parObjectType;

    #endregion
}