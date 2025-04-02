using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;
    
    private bool isFlash = false;
    private float interactionDistance = 4f;
    private LayerMask interactionLayer = LayerMask.GetMask("Interaction");
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
        input.playerActions.Attack.started += OnAttack;
        input.playerActions.Attack.canceled += OnAttack;
        input.playerActions.Interaction.started += OnInteraction;
        input.playerActions.Reload.started += OnReload;
    }


    protected virtual void RemoveinputActionCallbacks()
    {
        PlayerController input = stateMachine.player.Input;
        input.playerActions.Movement.canceled -= OnMovementCanceled;
        input.playerActions.Run.started -= OnRunStarted;
        input.playerActions.Jump.started -= OnJumpStarted;
        input.playerActions.Attack.started -= OnAttack;
        input.playerActions.Attack.canceled -= OnAttack;
        input.playerActions.Interaction.started -= OnInteraction;
        input.playerActions.Reload.started -= OnReload;

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
        Debug.DrawRay(stateMachine.MainCamTransform.position, stateMachine.MainCamTransform.forward * interactionDistance, Color.red, 1f);
        Move();
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context) { }

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
        if (stateMachine.GroundState != null)
        {
            if (stateMachine.MovementInput != Vector2.zero)
            {
                stateMachine.GroundState.ChangeSubState(stateMachine.GroundState.WalkState);
                stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
            }
            else
            {
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

    protected virtual void OnAttack(InputAction.CallbackContext context)
    {
        GunController gun = stateMachine.player.GetComponentInChildren<GunController>();
        if(gun != null)
        {
            if (context.started)
            {
            gun.GunAction.TriggerPull();
            Debug.Log("shoot");
            }
            if (context.canceled)
            {
            gun.GunAction.TriggerRelease();
            }
        }
    }
    protected virtual void OnReload(InputAction.CallbackContext context)
    {
        stateMachine.player.GetComponentInChildren<GunController>().GunAction.Reload();
    }

    protected virtual void OnInteraction(InputAction.CallbackContext context)
    {

        Debug.DrawRay(stateMachine.MainCamTransform.position, stateMachine.MainCamTransform.forward * interactionDistance, Color.green, 2f);

        Ray ray = new Ray(stateMachine.MainCamTransform.position, stateMachine.MainCamTransform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
            else
            {
                Debug.Log("이건 상호작용 안됨");
            }
        }
        else
        {
            Debug.Log("상호작용 할게 없음");
        }
    }
    public void FixedUpdate()
    {

    }
}
