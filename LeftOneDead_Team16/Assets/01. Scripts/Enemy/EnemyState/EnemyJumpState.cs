using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpState : EnemyBaseState
{
    public EnemyJumpState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.enemy.Jump();
        StartAnimation(stateMachine.enemy.enemyAnimaionData.JumpParameterName);
        stateMachine.enemy.animator.SetFloat("Speed", 1f);
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.enemy.enemyAnimaionData.JumpParameterName);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (stateMachine.enemy.navMeshAgent.velocity.y <= 0f)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }

}
