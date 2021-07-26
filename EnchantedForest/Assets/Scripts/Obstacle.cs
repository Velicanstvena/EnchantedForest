using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int x;
    private int y;

    private void Awake()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        Pathfinding.Instance.GetNode(x, y).SetIsWalkable(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
