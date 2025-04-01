using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerBaseState
{
    private readonly PlayerGroundState groundState;

    public PlayerRunState(PlayerStateMachine stateMachine, PlayerGroundState groundState) : base(stateMachine)
    {
        this.groundState = groundState;
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.player.Input.playerActions.Run.ReadValue<float>() <= 0)
        {
            if (stateMachine.MovementInput != Vector2.zero)
            {
                groundState.ChangeSubState(groundState.WalkState);
            }
            else
            {
                groundState.ChangeSubState(groundState.IdleState);
            }
        }
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
}
