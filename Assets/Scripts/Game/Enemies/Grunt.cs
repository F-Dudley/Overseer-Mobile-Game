using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
    #region Unity Functions
    protected void Start()
    {
        base.Init();
    }

    protected void Update()
    {
        if (Vector3.Distance(transform.position, GameManager.GameTarget) < attackRange)
        {
            agent.SetDestination(transform.position);
        }
    }
    #endregion

    protected override void AttackTarget()
    {

    }
}
