using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float lastAttackTime;
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.enemy.animator.SetTrigger("Attack");
    }

    public override void Update()
    {
        base.Update();
        Attack();
    }
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 공격 처리
    /// </summary>
    public void Attack()
    {
        float currentTime = Time.time;
        if(currentTime - lastAttackTime >= stateMachine.enemy.attackSpeed)
        {
            lastAttackTime = currentTime;
            Debug.Log("Attack");
        }
    }
    
}