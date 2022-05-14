using UnityEngine;

// ReSharper disable UnusedMember.Local

public class SoundsSystem : MonoBehaviour
{
    public GameObject soundParticle;
    public Transform createPoint;
    public Camera mainCamera;
    public int particleCount;
    public int attackAngel;
    public float cooldown;

    private bool canShoot = true;
    private float tempCooldown;

    void Update()
    {
        //Debug.Log(createPoint.GetComponentInChildren(typeof(createPoint)));
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            var spawnPos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - createPoint.position;
            float angle = attackAngel / 2;
            float angleIncrease = attackAngel / particleCount;
            for (var i = 0; i <= particleCount; i++)
            {
                var soundInstance = Instantiate(soundParticle, createPoint.position, Quaternion.Euler(0, 0, (Mathf.Atan2(spawnPos.y, spawnPos.x) * Mathf.Rad2Deg) + angle));
                soundInstance.GetComponent<Rigidbody2D>().AddForce(soundInstance.transform.right / 10);

                angle -= angleIncrease;
            }
            tempCooldown = cooldown * 50;
            canShoot = false;
        }
    }
    void FixedUpdate()
    {
        Cooldown();
    }

    private void Cooldown()
    {
        if (canShoot) return;
        tempCooldown--;
        if (tempCooldown <= 0)
        {
            canShoot = true;
        }
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        var angleRag = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRag), Mathf.Sign(angleRag));
    }
}