using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Uqdate()
    {
        base.Uqdate();

        if(stateMachine.player.Controller.isGrounded)
        {
            if (stateMachine.player.Input.playerActions.Run.ReadValue<float>() > 0)
            {
                stateMachine.ChageState(stateMachine.RunState);
            }
            else if (stateMachine.MovementInput != Vector2.zero)
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
