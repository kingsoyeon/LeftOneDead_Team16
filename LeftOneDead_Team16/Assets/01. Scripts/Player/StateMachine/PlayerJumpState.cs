using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.JumpForce = stateMachine.player.Data.AirData.JumpForce;
        stateMachine.player.ForceReceiver.Jump(stateMachine.JumpForce);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Uqdate()
    {
        base.Uqdate();

        if(stateMachine.player.Controller.velocity.y <= 0)
        {
            if(stateMachine.player.Input.playerActions.Run.ReadValue<float>() > 0)
            {
                stateMachine.ChageState(stateMachine.RunState);
            }
            else if(stateMachine.MovementInput != Vector2.zero)
            {
                stateMachine.ChageState(stateMachine.WalkState);
            }
            else
            {
                stateMachine.ChageState(stateMachine.IdleState);
            }
            return;
        }
    }
}
