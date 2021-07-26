using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private enum State
    {
        GoingToTheBase,
        ChasePlayer
    }

    private State state;
    private Attacker attacker;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    private const float speed = 1f;

    Base pBase;
    Player player;

    private void Awake()
    {
        state = State.GoingToTheBase;
    }

    // Start is called before the first frame update
    void Start()
    {
        attacker = FindObjectOfType<Attacker>();
        pBase = FindObjectOfType<Base>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        switch(state)
        {
            default:
            case State.GoingToTheBase:
                SetTargetPosition(pBase.GetBasePosition());
                attacker.ChangeStateToAttackBase();
                FindTarget();
                break;
            case State.ChasePlayer:
                SetTargetPosition(player.transform.position);
                attacker.ChangeStateToAttackPlayer();

                float stopChaseDistance = 3f;
                if (Vector3.Distance(transform.position, player.transform.position) > stopChaseDistance)
                {
                    // Too far, stop chasing
                    state = State.GoingToTheBase;
                }
                break;
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
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                    Destroy(gameObject);
                }
            }
        }
    }

    public void StopMoving()
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

    private void FindTarget()
    {
        float targetRange = 2f;
        if (Vector3.Distance(transform.position, player.transform.position) < targetRange)
        {
            // Player within target range
            state = State.ChasePlayer;
        }
    }
}
