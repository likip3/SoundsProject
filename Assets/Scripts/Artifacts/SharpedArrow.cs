using System.Collections;
using UnityEngine;

public class SharpedArrow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        col.GetComponentInParent<SoundsSystem>().damage += 30f;
        col.gameObject.GetComponentInParent<UserUI>().ShowMessage("Sharped Arrow", "Increase damage", true);
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