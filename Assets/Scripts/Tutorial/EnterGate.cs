using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGate : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (GetComponent<Rigidbody2D>().gravityScale == 0 && col.collider.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
            soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.white, 0.5f, 30, 1);
            GetComponent<AudioSource>().Play();
        }
    }
}
