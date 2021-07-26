using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private enum AttackState
    {
        Idle,
        AttackBase,
        AttackPlayer,
    }

    private AttackState currentState;

    EnemyMovement enemyMovement;

    [SerializeField] Projectile projectile;

    Vector2 moveDirection;
    float moveSpeed = 2f;

    Player player;
    Base pBase;
    Vector3 currentTarget;

    float fireRate;
    float nextFire;

    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();
    }

    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController != null)
        {
            levelController.AttackerKilled();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = AttackState.Idle;

        fireRate = 1f;
        nextFire = Time.time;

        player = FindObjectOfType<Player>();
        pBase = FindObjectOfType<Base>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            default:
            case AttackState.Idle:
                break;
            case AttackState.AttackBase:
                if (pBase)
                {
                    currentTarget = pBase.GetBasePosition();
                    if (CheckIfTargetInRange())
                    {
                        moveDirection = (pBase.transform.position - transform.position).normalized * moveSpeed;
                        CheckIfTimeToFire();
                    }
                }
                break;
            case AttackState.AttackPlayer:
                if (player)
                {
                    currentTarget = player.transform.position;
                    if (CheckIfTargetInRange())
                    {
                        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
                        CheckIfTimeToFire();
                    }
                }
                break;
        }
    }

    private void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Projectile firedProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            firedProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
            nextFire = Time.time + fireRate;
        }
    }

    private bool CheckIfTargetInRange()
    {
        float attackRange = 2f;
        if (Vector3.Distance(transform.position, currentTarget) < attackRange)
        {
            enemyMovement.StopMoving();
            return true;
        }

        return false;
    }

    public void ChangeStateToAttackPlayer()
    {
        currentState = AttackState.AttackPlayer;
    }

    public void ChangeStateToAttackBase()
    {
        currentState = AttackState.AttackBase;
    }

    public void ChangeStateToIdle()
    {
        currentState = AttackState.Idle;
    }
}
