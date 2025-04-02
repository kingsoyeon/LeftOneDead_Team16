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
        stateMachine.enemy.animator.SetBool("Idle", true);
        stateMachine.enemy.navMeshAgent.isStopped = true;
        idleTime = 0;
        allTime = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.enemy.animator.SetBool("Idle", false);
        stateMachine.enemy.navMeshAgent.isStopped = false;

    }

    float allTime = 0f;
    public override void Update()
    {
        base.Update();
        idleTime += Time.deltaTime;
        allTime += Time.deltaTime;

        // 타겟을 검색
        if(allTime > 0.1f){
            if (stateMachine.enemy.CheckTargetInSight()) return;
            allTime = 0f;
        }

        // 대기 상태 시간 증가
        

        // 대기 상태 시간이 지나면 순찰 상태로 변경
        if(idleTime >= stateMachine.enemy.patrolWaitTime)
        {
            stateMachine.ChangeState(stateMachine.PatrolState);
        }
    }
    
}