using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }

<<<<<<< HEAD
    
=======

>>>>>>> 8140256 (Merge branch 'CJM/Feat/Player' into Dev)
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
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChageState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
<<<<<<< HEAD
        stateMachine.Update();
=======
        stateMachine.Uqdate();
>>>>>>> 8140256 (Merge branch 'CJM/Feat/Player' into Dev)
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
