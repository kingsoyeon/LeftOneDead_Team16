using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    private IState currentGroundState;
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }

    private float groundCheckGraceTime = 0.1f;
    private float groundCheckTimer;

    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        IdleState = new PlayerIdleState(stateMachine, this);
        WalkState = new PlayerWalkState(stateMachine, this);
        RunState = new PlayerRunState(stateMachine, this);
    }
    public void HandleJumpInput()
    {
        stateMachine.AirState.EnterFromJumping();
        stateMachine.ChangeState(stateMachine.AirState);
    }

    public override void Enter()
    {
        base.Enter();
        groundCheckTimer = groundCheckGraceTime;

        bool isMoving = stateMachine.MovementInput != Vector2.zero;
        bool isRunning = stateMachine.player.Input.playerActions.Run.ReadValue<float>() > 0f;

        if (isMoving)
        {
            currentGroundState = isRunning ? RunState : WalkState;
        }
        else
        {
            currentGroundState = IdleState;
        }

        currentGroundState.Enter();
    }

    public override void Exit()
    {
        currentGroundState.Exit();

        
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        currentGroundState.Update();

        groundCheckTimer -= Time.deltaTime;

        if (groundCheckTimer <= 0f && !stateMachine.player.Controller.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.AirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        currentGroundState.FixedUpdate();
    }

    public void ChangeSubState(IState newSubState)
    {
        currentGroundState.Exit();
        currentGroundState = newSubState;
        currentGroundState.Enter();
        Debug.Log(currentGroundState);

    }
}
