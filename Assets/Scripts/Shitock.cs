using System.Collections;
using UnityEngine;

public class Shitock : MonoBehaviour
{
    [SerializeField] private GameObject turret;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        turret.GetComponent<AutoCannonScript>().disabled = true;
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    private IEnumerator WaveSpawner()
    {
        while (Application.isPlaying)
        {
            var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
            soundInstance.GetComponent<VibrationCircle>().OnCreate(
                transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Color.yellow, 0.1f,
                2.5f, 1);
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}