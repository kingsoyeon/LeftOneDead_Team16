using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field:SerializeField] private EnemySO enemySO;
    [field:SerializeField] private EnemyStateMachine stateMachine;

    public float moveSpeed => enemySO.MovementData.MoveSpeed;
    public float rotateSpeed => enemySO.MovementData.RotateSpeed;
    public float attackRange => enemySO.MovementData.AttackRange;
    public float traceRange => enemySO.MovementData.TraceRange;
    public float skillAttackRange => enemySO.MovementData.SkillAttackRange;

    public int baseHp => enemySO.StatData.BaseHp;
    public int baseAtk => enemySO.StatData.BaseAtk;
    public int baseDef => enemySO.StatData.BaseDef;


    public CharacterController characterController;
    private void Awake()
    {
        stateMachine = new EnemyStateMachine(this);
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

}