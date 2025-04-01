using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadState : PlayerBaseState
{
    private readonly PlayerReloadState reloadState;

    public PlayerReloadState(PlayerStateMachine stateMachine, PlayerAttackState attackState) : base(stateMachine)
    {
        this.reloadState = reloadState;
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
