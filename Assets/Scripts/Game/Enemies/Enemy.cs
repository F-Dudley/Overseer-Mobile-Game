using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Moving,
    Attacking
}

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Base Enemy")]
    [SerializeField] protected EnemyState currentState;

    [SerializeField] protected int health;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float rotationSpeed;

    [SerializeField] protected bool inAttackingPosition;

    [Space]

    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;

    [Header("Enemy Parts")]
    public Transform bodyTransform;
    public Transform weaponTransform;

    [Header("Components")]
    protected NavMeshAgent agent;

    #region Base Functions
    protected void Init()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected abstract void MoveState();
    protected abstract void AttackState();

    protected abstract void AttackTarget();

    protected virtual void UseAbility()
    {

    }

    protected abstract void StartMovementAnimation();
    protected abstract void StartAttackingAnimation();

    protected void StopMovementAnimation()
    {

    }

    #endregion
}
