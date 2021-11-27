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

    [Space]

    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;

    protected NavMeshAgent agent;

    #region Base Functions
    protected void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GameManager.GameTarget);
    }

    protected virtual void AttackTarget()
    {

    }

    protected virtual void UseAbility()
    {

    }
    #endregion
}