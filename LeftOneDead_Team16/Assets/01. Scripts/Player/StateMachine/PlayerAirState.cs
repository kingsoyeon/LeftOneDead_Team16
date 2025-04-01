using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    private IState currentAirState;

    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }

    public PlayerAirState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        JumpState = new PlayerJumpState(stateMachine, this);
        FallState = new PlayerFallState(stateMachine, this);
    }

    public override void Enter()
    {
        base.Enter();

        if (stateMachine.player.Controller.velocity.y > 0f)
        {
            currentAirState = JumpState;
        }
        else
        {
            currentAirState = FallState;
        }

        currentAirState.Enter();
    }

    public void EnterFromFalling()
    {
        currentAirState = FallState;
        currentAirState.Enter();
    }

    public void EnterFromJumping()
    {
        currentAirState = JumpState;
        currentAirState.Enter();
    }

    public override void Exit()
    {
        currentAirState.Exit();
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        currentAirState.Update();

        if (stateMachine.player.Controller.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.GroundState);
        }
    }

    public override void PhysicsUpdate()
    {
        currentAirState.FixedUpdate();
    }

    public void ChangeSubState(IState newSubState)
    {
        currentAirState.Exit();
        currentAirState = newSubState;
        currentAirState.Enter();
        Debug.Log(currentAirState);

    }
}
