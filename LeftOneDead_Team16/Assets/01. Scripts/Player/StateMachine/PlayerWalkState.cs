using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerBaseState
{
    private readonly PlayerGroundState groundState;

    public PlayerWalkState(PlayerStateMachine stateMachine, PlayerGroundState groundState) : base(stateMachine)
    {
        this.groundState = groundState;
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.player.Input.playerActions.Run.ReadValue<float>() > 0f)
        {
            groundState.ChangeSubState(groundState.RunState);
        }
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        base.Enter();
    }
}
