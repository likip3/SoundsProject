using System.Collections;
using UnityEngine;

public class SolderSergury : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        var system = col.gameObject.GetComponentInParent<SoundsSystem>();
        system.cooldown -= system.cooldown * 0.15f;
        system.particleSpeed += system.particleSpeed * 0.05f;
        col.gameObject.GetComponentInParent<UserUI>()
            .ShowMessage("Solder Syringe", "Shot Rate UP \n Shot Speed UP", true);
        var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
        soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.cyan, 1f, 20f, 1);

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
            soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.cyan, 0.1f, 2.5f, 1);
            yield return new WaitForSecondsRealtime(5f);
        }
    }
}