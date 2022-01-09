using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grunt : Enemy
{
    #region Unity Functions
    protected void Start()
    {
        base.Init();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        agent.SetDestination(GameManager.AssaultTarget);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
    #endregion

    #region States
    protected override void AttackState()
    {
        if (inAttackingPosition)
        {
            AttackTarget();
        }
    }

    protected override void MoveState()
    {
        if (Vector3.Distance(transform.position, GameManager.AssaultTarget) < attackRange)
        {
            agent.isStopped = true;
            currentState = EnemyState.Attacking;
            transform.DOKill(false);
            StartAttackingAnimation();
        }
    }
    #endregion

    #region State Actions
    protected override void AttackTarget()
    {
        
    }

    protected override async void StartMovementAnimation()
    {
        await bodyTransform.DORotate(new Vector3(0, -10, -5), 0.5f).AsyncWaitForCompletion();
        bodyTransform.DORotate(new Vector3(0, 10, 5), 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    protected override async void StartAttackingAnimation()
    {
        await bodyTransform.DORotate(Vector3.zero, 0.5f).AsyncWaitForCompletion();
        await weaponTransform.DORotate(new Vector3(90, 0, 0), 1.5f).AsyncWaitForCompletion();
        inAttackingPosition = true;
    }

    public override void ReturnToPool()
    {
        gameObject.SetActive(false);
        GameplayManager.instance.assaultPool.ReturnItem(this.gameObject);
    }
    #endregion
}
