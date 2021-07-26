using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Pathfinding pathfinding;
    private Pathfinding2 pathfinding2;

    int numberOfAttackers = 0;
    bool levelTimerFinished = false;


    private void Awake()
    {
        pathfinding = new Pathfinding(9, 5);
        pathfinding2 = new Pathfinding2();
    }

    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }

    public void AttackerKilled()
    {
        numberOfAttackers--;
        if (numberOfAttackers <= 0 && levelTimerFinished)
        {
            FindObjectOfType<LevelLoader>().LoadNextScene();
        }
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        EnemySpawner[] spawnerArray = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }
}
