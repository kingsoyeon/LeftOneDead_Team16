using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private PlayerAirState airState;

    public PlayerJumpState(PlayerStateMachine stateMachine, PlayerAirState airState) : base(stateMachine)
    {
        this.airState = airState;
    }

    public override void Enter()
    {
        stateMachine.JumpForce = stateMachine.player.Data.AirData.JumpForce;
        stateMachine.player.ForceReceiver.Jump(stateMachine.JumpForce);
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.player.Controller.velocity.y <= 0)
        {
            airState.ChangeSubState(airState.FallState);
        }
    }
}
