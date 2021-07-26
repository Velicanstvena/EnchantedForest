using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private Pathfinding pathfinding;
    //private Pathfinding2 pathfinding2;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    private const float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        //pathfinding = new Pathfinding(9, 5);
        //pathfinding2 = new Pathfinding2();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            
            SetTargetPosition(mouseWorldPosition);
        }
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            } else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
