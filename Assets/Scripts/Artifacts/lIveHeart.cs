using System.Collections;
using UnityEngine;

public class lIveHeart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        col.gameObject.GetComponentInParent<HealthSystem>().regeneration += 1;
        col.gameObject.GetComponentInParent<UserUI>().ShowMessage("lIve Heart", "Increases regeneration", true);
        var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
        soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.red, 1f, 20f, 1);

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
            soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.red, 0.1f, 2.5f, 1);
            yield return new WaitForSecondsRealtime(5f);
        }
    }
}