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
        Debug.Log("IdleState");
        idleTime = 0;
    }

    public override void Update()
    {
        base.Update();

        // 타겟을 검색
        stateMachine.enemy.FindNearestTarget();

        // 대기 상태 시간 증가
        idleTime += Time.deltaTime;

        // 대기 상태 시간이 지나면 순찰 상태로 변경
        if(idleTime >= stateMachine.enemy.patrolWaitTime)
        {
            stateMachine.ChangeState(stateMachine.PatrolState);
        }
    }
    
}