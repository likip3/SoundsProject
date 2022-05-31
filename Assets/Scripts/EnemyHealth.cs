using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator _animator;
    public float health;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void GetHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            _animator.SetTrigger("DeathTR");
            _animator.SetBool("Death", true);
        }
    }
}