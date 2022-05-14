using UnityEngine;

public class SpawnVibro : MonoBehaviour
{
    public GameObject soundWave;
    public int lifePunches;

    public void OnCollisionEnter2D(Collision2D col)
    {
        Instantiate(soundWave, transform.position, new Quaternion(0, 0, 0, 0));
        lifePunches--;
        if (lifePunches<=0)
        {
            Destroy(transform.gameObject);
        }
    }
}
