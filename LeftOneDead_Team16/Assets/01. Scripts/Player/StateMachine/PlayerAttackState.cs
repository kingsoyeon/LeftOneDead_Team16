using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private IState currentAttackState;

    public PlayerShootState shootState {  get; private set; }
    public PlayerReloadState reloadState { get; private set; }

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        shootState = new PlayerShootState(stateMachine, this);
        reloadState = new PlayerReloadState(stateMachine, this);
    }

    public override void Enter()
    {
        Debug.Log("AttackState 진입");
        base.Enter();

    }

    public override void Update()
    {
        base.Update();

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("AttackState 종료");
    }
}

