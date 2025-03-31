using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour, IDamageable
{
    [field:SerializeField] private EnemySO enemySO;
    [field:SerializeField] private EnemyStateMachine stateMachine;
    public CharacterController characterController;
    public Animator animator; // 애니메이터
    public EnemyAnimaionData enemyAnimaionData; // 애니메이션 데이터

    public Skill skill;

    public float moveSpeed => enemySO.MovementData.MoveSpeed;
    public float rotateSpeed => enemySO.MovementData.RotateSpeed;
    public float attackRange => enemySO.MovementData.AttackRange;
    public float traceRange => enemySO.MovementData.TraceRange;
    public float patrolTime => enemySO.MovementData.PatrolTime;
    public float patrolWaitTime => enemySO.MovementData.PatrolWaitTime;
    public float skillAttackRange => enemySO.MovementData.SkillAttackRange;
    public float attackSpeed => enemySO.MovementData.AttackSpeed;
    public float skillSpeed => enemySO.MovementData.SkillAttackSpeed;

    public float detectionRange => enemySO.MovementData.DetectionRange;

    public int baseHp => enemySO.StatData.BaseHp;
    public int baseAtk => enemySO.StatData.BaseAtk;
    public int baseDef => enemySO.StatData.BaseDef;

    public Transform target;    // 적의 타겟
    public LayerMask targetLayer;

    public int curHp{get; private set;}

    public NavMeshAgent navMeshAgent{get; private set;}

    void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
        targetLayer = LayerMask.GetMask("Player");  // 타겟 레이어 설정
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        stateMachine = new EnemyStateMachine(this);
        navMeshAgent = GetComponent<NavMeshAgent>();

        enemyAnimaionData.Initialize();
        curHp = baseHp;

        skill = GetComponent<Skill>();
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


    // 일정 위치로 무조건 가게 하는 함수
    public void MoveToPosition(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }

    /// <summary>
    /// 타겟을 찾았을 때 스테이트를 TraceState로 변경
    /// </summary>
    public void DetectTarget()
    {
        Debug.Log("DetectTarget");
        if(target == null)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.TraceState);
        
    }

    /// <summary>
    /// 가장 가까운 타겟을 찾는 함수
    /// </summary>
    public bool FindNearestTarget()
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

        if(target != null)
        {
            DetectTarget();
            return true;
        }
        
        return false;

    }


    /// <summary>
    /// 데미지 처리         
    /// </summary>
    /// <param name="damage">데미지</param>
    public void TakeDamage(int damage)
    {
        float damageMultiplier = 100f / (100f + baseDef);
        // 데미지 받기 방어력 적용해서 데미지 계산
        damage = Mathf.Max(Mathf.RoundToInt(damage * damageMultiplier), 1);
       
        // 데미지 처리
        curHp -= damage;

        // 체력이 0이하면 죽음
        if(curHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Die");
        animator.SetBool("Die", true);
    }
}