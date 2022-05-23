using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
    public Image image;
    public float maxHealth;
    public float regeneration;
    public float health;
    public bool immortal;

    void Start()
    {
        health = maxHealth;
    }

    void FixedUpdate()
    {
        //Debug.Log(tempHealth);

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    GetHit(100, new Vector2());
        //}

        if (health < maxHealth)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, (float)(1 - health / maxHealth - 0.3));
            health += regeneration;
        }

    }

    public void GetHit(float damage)
    {
        if (immortal) return;
        health -= damage;
        image.color = new Color(image.color.r, image.color.g, image.color.b, (float)(1 - health / maxHealth - 0.3));
    }
}
