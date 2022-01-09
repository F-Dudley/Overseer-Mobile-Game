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
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float rotationSpeed;

    [SerializeField] protected bool inAttackingPosition;

    [Space]

    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;

    [Header("Enemy Parts")]
    [SerializeField] protected Transform bodyTransform;
    [SerializeField] protected Transform weaponTransform;

    [Header("Components")]
    protected NavMeshAgent agent;

    #region Unity Functions
    protected virtual void OnEnable()
    {
        StartMovementAnimation();
    }

    protected virtual void OnDisable()
    {
        transform.DOKill(false);
    }
    #endregion

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

    #region Interaction Functions
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        if (health <= 0) ReturnToPool();
    }

    public void Heal(int _regainedHealth)
    {
        health += _regainedHealth;
    }

    public abstract void ReturnToPool();
    #endregion
}
