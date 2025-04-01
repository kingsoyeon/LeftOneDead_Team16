using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }
    public Transform MainCamTransform { get; set; }

    public PlayerGroundState GroundState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerAttackState attackState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.player = player;

        MainCamTransform = Camera.main.transform;
        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDaping;

        GroundState = new PlayerGroundState(this);
        AirState = new PlayerAirState(this);
        attackState = new PlayerAttackState(this);
    }

    public void HandleInput()
    {
        MovementInput = player.Input.playerActions.Movement.ReadValue<Vector2>();
    }

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void PhysicsUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
