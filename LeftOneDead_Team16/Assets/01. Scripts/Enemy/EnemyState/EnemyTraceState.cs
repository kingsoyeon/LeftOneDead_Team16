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
       // Debug.Log("TraceState");
        base.Enter();
        stateMachine.enemy.animator.SetBool("Move", true);
        stateMachine.enemy.animator.SetFloat("Speed", 1f);

        stateMachine.enemy.navMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();
        Trace();
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.enemy.animator.SetBool("Move", false);
        stateMachine.enemy.animator.SetFloat("Speed", 0f);
        stateMachine.enemy.navMeshAgent.isStopped = false;
    }
    
    /// <summary>
    /// 추적 처리
    /// </summary>
    public void Trace()
    {
        // Debug.Log("Trace");
        if(stateMachine.enemy.target != null)   
        { 
            // 타겟이 있으면 추적 처리
            if(Vector3.Distance(stateMachine.enemy.transform.position, stateMachine.enemy.target.position) > stateMachine.enemy.attackRange - 0.1f)
            {
                // 공격 범위 밖에 있으면 추적 처리
                stateMachine.enemy.navMeshAgent.SetDestination(stateMachine.enemy.target.position);
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
