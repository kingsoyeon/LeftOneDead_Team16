using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.player.Data.GroundData;
    }

    public virtual void Enter()
    {
        AddinputActionCallbacks();
    }

    public virtual void Exit()
    {
        RemoveinputActionCallbacks();
    }

    protected virtual void AddinputActionCallbacks()
    {
        PlayerController input = stateMachine.player.Input;
        input.playerActions.Movement.canceled += OnMovementCanceled;
        input.playerActions.Run.started += OnRunStarted;
        input.playerActions.Jump.started += OnJumpStarted;
    }

    protected virtual void RemoveinputActionCallbacks()
    {
        PlayerController input = stateMachine.player.Input;
        input.playerActions.Movement.canceled -= OnMovementCanceled;
        input.playerActions.Run.started -= OnRunStarted;
        input.playerActions.Jump.started -= OnJumpStarted;
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput != Vector2.zero && stateMachine.GroundState != null)
        {
            stateMachine.GroundState.ChangeSubState(stateMachine.GroundState.RunState);
        }
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (!stateMachine.player.Controller.isGrounded) return;

        if (stateMachine.GroundState != null)
        {
            stateMachine.GroundState.HandleJumpInput();
        }
    }
    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("▶ Run canceled!");

        if (stateMachine.GroundState != null)
        {
            if (stateMachine.MovementInput != Vector2.zero)
            {
                Debug.Log("→ Switching to WalkState");
                stateMachine.GroundState.ChangeSubState(stateMachine.GroundState.WalkState);
                stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
            }
            else
            {
                Debug.Log("→ Switching to IdleState");
                stateMachine.GroundState.ChangeSubState(stateMachine.GroundState.IdleState);
                stateMachine.MovementSpeedModifier = 1f;
            }
        }
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.player.Input.playerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDiretion = GetMovementDirection();

        Move(movementDiretion);

        Rotate(movementDiretion);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCamTransform.forward;
        Vector3 right = stateMachine.MainCamTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.player.Controller.Move(((direction * movementSpeed) + stateMachine.player.ForceReceiver.Movement) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    private void Rotate(Vector3 direction)
    {
        float targetYRotation = stateMachine.MainCamTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0f, targetYRotation, 0f);
        stateMachine.player.transform.rotation = targetRotation;
    }

    public void FixedUpdate()
    {

    }
}
