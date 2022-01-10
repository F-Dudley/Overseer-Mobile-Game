using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

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
    [SerializeField] protected int maxHealth;
    [SerializeField] protected bool inAttackingPosition;

    [Space]

    [SerializeField] protected bool attackReady;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;

    [Header("Enemy Parts")]
    [SerializeField] protected Transform bodyTransform;
    [SerializeField] protected Transform weaponTransform;

    [Header("Components")]
    protected NavMeshAgent agent;

    #region Unity Functions
    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected void Update()
    {
        switch (currentState)
        {
            case EnemyState.Moving:
                MoveState();
                break;

            case EnemyState.Attacking:
                AttackState();
                break;

            default:
            break;
        }
    }

    protected void OnEnable()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        StartMovementAnimation();
        agent.isStopped = false;
    }

    protected void OnDisable()
    {
        health = maxHealth;
        transform.DOKill(false);
        GameplayManager.instance.ActiveEnemyDied();
        ReturnToPool();
    }
    #endregion

    #region Base Functions
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

    #region Interaction Functions
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        if (health <= 0) gameObject.SetActive(false);
    }

    public void Heal(int _regainedHealth)
    {
        health += _regainedHealth;
    }

    public abstract void ReturnToPool();
    #endregion
}
