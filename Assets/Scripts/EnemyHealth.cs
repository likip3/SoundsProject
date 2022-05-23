using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float regeneration;
    public float health;

    public void GetHit(float damage)
    {
        health -= damage;
        if (health<=0)
        {

        }
    }
}
