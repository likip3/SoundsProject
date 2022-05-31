using UnityEngine;

public class GravityParticle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Gravity"))
            col.GetComponent<SoundControl>().SoundParticles.Add(gameObject);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Gravity"))
            col.GetComponent<SoundControl>().SoundParticles.Remove(gameObject);
    }
}