using UnityEngine;

public class BoostLine : MonoBehaviour
{
    [SerializeField] private Vector2 boost;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        col.GetComponent<Rigidbody2D>().AddForce(boost);
    }
}