using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private PlayerAirState airState;

    public PlayerFallState(PlayerStateMachine stateMachine, PlayerAirState airState) : base(stateMachine)
    {
        this.airState = airState;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }
}