using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadState : PlayerBaseState
{
    private readonly PlayerAttackState attackState;

    public PlayerReloadState(PlayerStateMachine stateMachine, PlayerAttackState attackState) : base(stateMachine)
    {
        this.attackState = attackState;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
