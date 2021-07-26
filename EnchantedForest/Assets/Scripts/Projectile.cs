using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage = 100f;

    private void Awake()
    {
        damage += (PlayerPrefsController.GetDifficulty() * 50f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attacker attacker = collision.gameObject.GetComponent<Attacker>();
        Health health = collision.GetComponent<Health>();

        if (!attacker && health)
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
