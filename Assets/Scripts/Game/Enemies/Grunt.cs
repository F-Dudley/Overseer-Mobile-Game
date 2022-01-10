using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grunt : Enemy
{
    #region Unity Functions
    protected new void Start()
    {
        base.Start();
    }

    protected new void OnEnable()
    {
        base.OnEnable();
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        ReturnToPool();
    }

    protected new void Update()
    {
        base.Update();
    }
    #endregion

    #region States
    protected override void AttackState()
    {
        if (inAttackingPosition && attackReady)
        {
            AttackTarget();
        }
    }

    protected override void MoveState()
    {
        if (Vector3.Distance(transform.position, GameManager.AssaultTarget) < attackRange)
        {
            currentState = EnemyState.Attacking;
            transform.DOKill(false);
            StartAttackingAnimation();
        }
    }
    #endregion

    #region State Actions
    protected override void AttackTarget()
    {
        attackReady = false;
        // GameplayManager.instance.DamageBase((int) damage);
    }

    protected override async void StartMovementAnimation()
    {
        await bodyTransform.DOLocalRotate(new Vector3(0, -10, -5), 0.5f).AsyncWaitForCompletion();
        bodyTransform.DOLocalRotate(new Vector3(0, 10, 5), 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    protected override async void StartAttackingAnimation()
    {
        bodyTransform.DOKill();
        await bodyTransform.DOLocalRotate(Vector3.zero, 0.5f).AsyncWaitForCompletion();
        await weaponTransform.DOLocalRotate(new Vector3(-90, 0, 0), 1.5f).AsyncWaitForCompletion();
        inAttackingPosition = true;
    }

    public override void ReturnToPool()
    {
        GameplayManager.instance.ResourcesAmount += 1;

        gameObject.SetActive(false);
        GameplayManager.instance.enemyPool.ReturnAssaultItem(this.gameObject);
    }
    #endregion
}
