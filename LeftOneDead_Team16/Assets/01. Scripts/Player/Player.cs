using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [field: SerializeField] public PlayerSO Data { get; private set; }
    
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public CameraController cameraController { get; private set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        cameraController = GetComponent<CameraController>();

        stateMachine = new PlayerStateMachine(this);
    }
    void Start()
    {
        //체력 초기화
        Data.Condition.currentHealth = Data.Condition.maxHealth;
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

    //데미지를 받으면 호출되는 함수
    public void TakeDamage(int damage)
    {
        stateMachine.player.Data.Condition.currentHealth -= damage;

        if(Data.Condition.currentHealth <= 0 )
        {
            UIManager.Instance.ShowPopup<GameOverUI>("GameOverUI");
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
