using UnityEngine;

public class StompMachine : MonoBehaviour
{
    private AudioSource _audio;
    private Animator Animator;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
    }

    private void SpawnWave()
    {
        var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
        soundInstance.GetComponent<VibrationCircle>()
            .OnCreate(transform.position + new Vector3(1, 0), Color.white, 1f, 20f, 0.5f);
        _audio.Play();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player")) Animator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) Animator.enabled = true;
    }
}