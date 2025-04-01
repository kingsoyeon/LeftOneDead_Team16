using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    private readonly PlayerGroundState groundState;

    public PlayerIdleState(PlayerStateMachine stateMachine, PlayerGroundState groundState) : base(stateMachine)
    {
        this.groundState = groundState;
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput != Vector2.zero)
        {
            bool isRunning = stateMachine.player.Input.playerActions.Run.ReadValue<float>() > 0f;

            if (isRunning)
            {
                groundState.ChangeSubState(groundState.RunState);
            }
            else
            {
                groundState.ChangeSubState(groundState.WalkState);
            }
        }
    }
}
