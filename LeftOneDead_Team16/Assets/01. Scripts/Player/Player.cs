using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [field: SerializeField] public PlayerSO Data { get; private set; }

    
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }


    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new PlayerStateMachine(this);
    }
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.GroundState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public void TakeDamage(int damage)
    {
        stateMachine.player.Data.Condition.currentHealth -= damage;
        Debug.Log(stateMachine.player.Data.Condition.currentHealth);
    }
}
