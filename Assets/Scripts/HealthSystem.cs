using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float damageReduce;
    public float health;
    public Image image;
    public bool immortal;
    public bool isDead = false;
    public float maxHealth;
    public float regeneration;

    private void Start()
    {
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if (GetComponentInParent<HealthSystem>().isDead) return;
        if (health < maxHealth)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, (float)(1 - health / maxHealth - 0.3));
            health += regeneration;
        }
    }

    public void GetHit(float damage)
    {
        if (immortal) return;
        health -= damage / damageReduce;
        image.color = new Color(image.color.r, image.color.g, image.color.b, (float)(1 - health / maxHealth - 0.3));
        if (health <= 0)
        {
            var temp = GetComponentsInChildren<HingeJoint2D>();
            foreach (var joint2D in temp)
                joint2D.useLimits = false;

            isDead = true;
        }
    }
}