using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private float idleTime;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        idleTime = stateMachine.enemy.patrolWaitTime;
    }

    public override void Enter()
    {
        base.Enter();
        idleTime = 0;
    }

    public override void Update()
    {
        base.Update();
        stateMachine.enemy.FindNearestTarget();
        
        idleTime += Time.deltaTime;
        if(idleTime >= stateMachine.enemy.patrolWaitTime)
        {
            stateMachine.ChangeState(stateMachine.PatrolState);
        }
    }
    
}