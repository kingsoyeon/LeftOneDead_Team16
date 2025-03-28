using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [field:SerializeField] private EnemySO enemySO;
    [field:SerializeField] private EnemyStateMachine stateMachine;
    public CharacterController characterController;
    public Animator animator; // 애니메이터
    public EnemyAnimaionData enemyAnimaionData; // 애니메이션 데이터


    public float moveSpeed => enemySO.MovementData.MoveSpeed;
    public float rotateSpeed => enemySO.MovementData.RotateSpeed;
    public float attackRange => enemySO.MovementData.AttackRange;
    public float traceRange => enemySO.MovementData.TraceRange;
    public float patrolTime => enemySO.MovementData.PatrolTime;
    public float patrolWaitTime => enemySO.MovementData.PatrolWaitTime;
    public float skillAttackRange => enemySO.MovementData.SkillAttackRange;
    public float attackSpeed => enemySO.MovementData.AttackSpeed;

    public float detectionRange => enemySO.MovementData.DetectionRange;

    public int baseHp => enemySO.StatData.BaseHp;
    public int baseAtk => enemySO.StatData.BaseAtk;
    public int baseDef => enemySO.StatData.BaseDef;

    public Transform target;    // 적의 타겟
    public LayerMask targetLayer;


    public NavMeshAgent navMeshAgent{get; private set;}

    void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetLayer = LayerMask.GetMask("Player");  // 타겟 레이어 설정
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        stateMachine = new EnemyStateMachine(this);
        enemyAnimaionData.Initialize();
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

    /// <summary>
    /// 타겟을 찾았을 때 스테이트를 TraceState로 변경
    /// </summary>
    public void DetectTarget()
    {
        Debug.Log("DetectTarget");
        if(Vector3.Distance(transform.position, target.position) <= traceRange)
        {
            stateMachine.ChangeState(stateMachine.TraceState);
        }
    }

    /// <summary>
    /// 가장 가까운 타겟을 찾는 함수
    /// </summary>
    public void FindNearestTarget()
    {
        Debug.Log("FindNearestTarget");
        // 감지 범위 내의 모든 타겟 찾기
        Collider[] colliders = Physics.OverlapSphere(stateMachine.enemy.transform.position, stateMachine.enemy.detectionRange, stateMachine.enemy.targetLayer);
        float nearestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            // 나중에 타겟이 플레이어인지 확인?하면 좋을듯
            float distance = Vector3.Distance(stateMachine.enemy.transform.position, collider.transform.position);
            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                stateMachine.enemy.target = collider.transform;
            }
        }
        // 타겟이 있으면 타겟을 찾았다고 판단하고 스테이트를 TraceState로 변경
        DetectTarget();

    }
}