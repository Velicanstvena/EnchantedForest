using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 5f;
    [SerializeField] float maxSpawnDelay = 10f;
    [SerializeField] Attacker enemyPrefab;

    bool spawn = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            Spawn();
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void Spawn()
    {
        Attacker newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        newEnemy.transform.parent = transform;
    }
}
