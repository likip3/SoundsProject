using System.Collections;
using UnityEngine;

public class APieceOfBrain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        col.gameObject.GetComponentInParent<HealthSystem>().maxHealth *= 1.3f;
        col.gameObject.GetComponentInParent<UserUI>().ShowMessage("A Piece Of Brain", "Looks tasty", true);
        var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
        soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.magenta, 1f, 20f, 1);
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
            soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.magenta, 0.1f, 2.5f, 1);
            yield return new WaitForSecondsRealtime(5f);
        }
    }
}