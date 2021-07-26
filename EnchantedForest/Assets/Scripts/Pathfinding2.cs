using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding2
{
    PathNode startWaypoint, endWaypoint;

    private Grid<PathNode> gridInList;
    Dictionary<Vector2Int, PathNode> grid = new Dictionary<Vector2Int, PathNode>();
    Queue<PathNode> queue = new Queue<PathNode>();
    bool isRunning = true;
    PathNode searchCenter;
    List<PathNode> path = new List<PathNode>();
    List<Vector3> pathInVector3 = new List<Vector3>();

    public static Pathfinding2 Instance { get; private set; }

    public Pathfinding2()
    {
        Instance = this;

        gridInList = Pathfinding.Instance.GetGrid();

        for (int x = 0; x < gridInList.GetWidth(); x++)
        {
            for (int y = 0; y < gridInList.GetHeight(); y++)
            {
                PathNode pn = Pathfinding.Instance.GetNode(x ,y);
                Vector2Int pnPos = pn.GetGridPosInVector2Int();
                if (grid.ContainsKey(pnPos))
                {
                    Debug.LogWarning("Duplicate " + pn.ToString());
                }
                else
                {
                    grid.Add(pnPos, pn);
                }
            }
        }
        
    }

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Vector3> GetPath(Vector3 startNode, Vector3 endNode)
    {
        startWaypoint = Pathfinding.Instance.GetNode((int)startNode.x, (int)startNode.y);
        endWaypoint = Pathfinding.Instance.GetNode((int)endNode.x, (int)endNode.y);

        BreadthFirstSearch();
        CreatePath();

        foreach(PathNode p in path)
        {
            pathInVector3.Add(p.GetGridPosInVector3() + Vector3.one * 0.5f);
            Debug.Log(p.ToString());
        }

        //return path;
        //endWaypoint = startWaypoint;
        return pathInVector3;
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);

        PathNode previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);
        path.Reverse();

    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndPoint();
            ExploreNeighbours();
        }
    }

    private void HaltIfEndPoint()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPosInVector2Int() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        PathNode neighbour = grid[neighbourCoordinates];

        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }
}
