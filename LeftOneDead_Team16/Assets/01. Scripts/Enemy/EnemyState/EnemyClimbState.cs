using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClimbState : EnemyBaseState
{
    public EnemyClimbState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.enemy.enemyAnimaionData.ClimbParameterName);
        stateMachine.enemy.StartClimb();
        Debug.Log("벽을 오르는 상태 진입");
    }   

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.enemy.enemyAnimaionData.ClimbParameterName);
    }

    public override void Update()
    {
        base.Update();
        if(!stateMachine.enemy.navMeshAgent.isStopped)
        {
            Debug.Log("벽을 오르는 상태 종료");
            stateMachine.ChangeState(stateMachine.beforeState);
        }
    }
    
}
