using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 50f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attacker attacker = collision.gameObject.GetComponent<Attacker>();
        Health health = collision.GetComponent<Health>(); // moze i var health
        if (attacker && health)
        {
            health.DealDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
