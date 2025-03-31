using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.RunSpeedModifier;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Uqdate()
    {
        base.Uqdate();

        if(stateMachine.player.Input.playerActions.Run.ReadValue<float>() <= 0)
        {
            if(stateMachine.MovementInput != Vector2.zero)
            {
                stateMachine.ChageState(stateMachine.WalkState);
            }
            else
            {
                stateMachine.ChageState(stateMachine.IdleState);
            }
        }
    }
}
