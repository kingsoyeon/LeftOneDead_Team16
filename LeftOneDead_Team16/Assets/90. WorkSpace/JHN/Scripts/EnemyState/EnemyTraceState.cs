using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraceState : EnemyBaseState
{
    public EnemyTraceState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("TraceState");
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        Trace();
    }

    /// <summary>
    /// 추적 처리
    /// </summary>
    public void Trace()
    {
        Debug.Log("Trace");
        if(stateMachine.enemy.target != null)   
        { 
            // 타겟이 있으면 추적 처리
            if(Vector3.Distance(stateMachine.enemy.transform.position, stateMachine.enemy.target.position) > stateMachine.enemy.attackRange)
            {
                // 공격 범위 밖에 있으면 추적 처리
                stateMachine.enemy.navMeshAgent.SetDestination(stateMachine.enemy.target.position);
                Debug.Log($"Tracing : {stateMachine.enemy.target}");    
            }
            else
            {
                // 공격 범위 내에 들어오면 공격 상태로 변경
                stateMachine.enemy.navMeshAgent.isStopped = true;
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }
        else
        {
            // 타겟이 없으면 대기 상태로 변경
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }


}
