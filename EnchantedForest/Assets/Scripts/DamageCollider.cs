using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Attacker>())
        {
            FindObjectOfType<LevelLoader>().LoadYouLose();
            Destroy(collision.gameObject);
        }
        //FindObjectOfType<LivesDisplay>().TakeLife();
        
    }
}
