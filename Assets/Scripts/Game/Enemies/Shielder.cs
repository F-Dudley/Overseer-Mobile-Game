using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shielder : Enemy
{
    [Header("Shield Variables")]
    [SerializeField] private Transform shieldContainer;
    [SerializeField] private float shieldFullRotateSpeed = 3f;

    #region Unity Functions
    protected new void Start()
    {
        base.Start();
    }

    protected new void OnEnable()
    {
        base.OnEnable();

        StartAttackingAnimation();
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        ResetShields();
    }

    protected new void Update()
    {
        base.Update();
    }
    #endregion

    #region Enemy States
    protected override void MoveState()
    {
        if (Vector3.Distance(transform.position, GameManager.SupportTarget) < attackRange)
        {
            currentState = EnemyState.Attacking;
        }
    }

    protected override void AttackState()
    {
    
    }

    protected override void AttackTarget()
    {

    }
    protected override void UseAbility()
    {
    
    }
    #endregion

    #region Animations
    protected override void StartMovementAnimation()
    {
    
    }

    protected override void StartAttackingAnimation()
    {
        shieldContainer.DORotate(new Vector3(0, 360, 0), shieldFullRotateSpeed).SetLoops(-1, LoopType.Restart);
    }

    protected void ResetShields()
    {
        shieldContainer.rotation = Quaternion.identity;
    }
    #endregion

    public override void ReturnToPool()
    {
        GameplayManager.instance.ResourcesAmount += 3;

        gameObject.SetActive(false);
        GameplayManager.instance.enemyPool.ReturnSupportItem(this.gameObject);
    }
}
