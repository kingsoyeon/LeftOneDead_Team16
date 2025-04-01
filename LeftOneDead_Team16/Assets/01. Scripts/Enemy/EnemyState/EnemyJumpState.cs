using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpState : EnemyBaseState
{
    float jumpTime = 0f;
    float jumpMaxTime = 3f;
    public EnemyJumpState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.enemy.Jump();
        StartAnimation(stateMachine.enemy.enemyAnimaionData.JumpParameterName);
        stateMachine.enemy.animator.SetFloat("Speed", 1f);
        jumpTime = 0f;
        Debug.Log("점프 상태 진입");
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.enemy.enemyAnimaionData.JumpParameterName);
    }

    public override void Update()
    {
        base.Update();
        jumpTime += Time.deltaTime;
        if(jumpTime > jumpMaxTime)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
