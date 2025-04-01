using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStartState
{
    Idle,
    Trace,
}


public class Enemy : MonoBehaviour, IDamageable
{
    [field:SerializeField] private EnemySO enemySO;
    [field:SerializeField] private EnemyStateMachine stateMachine;
    public CharacterController characterController;
    public Animator animator; // 애니메이터
    [field: SerializeField] public EnemyAnimaionData enemyAnimaionData{get; private set;} // 애니메이션 데이터


    public Skill skill; // 스킬

    public EnemyStartState startState = EnemyStartState.Idle;  // 시작 상태 정해주기 => 기본 대기

    public float moveSpeed => enemySO.MovementData.MoveSpeed;
    public float runSpeed => enemySO.MovementData.RunSpeed;
    public float rotateSpeed => enemySO.MovementData.RotateSpeed;
    public float jumpForce => enemySO.MovementData.JumpForce;
    public float attackRange => enemySO.MovementData.AttackRange;
    public float traceRange => enemySO.MovementData.TraceRange;
    public float patrolTime => enemySO.MovementData.PatrolTime;
    public float patrolWaitTime => enemySO.MovementData.PatrolWaitTime;
    public float skillAttackRange => enemySO.MovementData.SkillAttackRange;
    public float attackSpeed => enemySO.MovementData.AttackSpeed;
    public float skillSpeed => enemySO.MovementData.SkillAttackSpeed;

    public float detectionRange => enemySO.MovementData.DetectionRange;
    public float detectionAngle => enemySO.MovementData.DetectionAngle;

    public int baseHp => enemySO.StatData.BaseHp;
    public int baseAtk => enemySO.StatData.BaseAtk;
    public int baseDef => enemySO.StatData.BaseDef;

    public Transform target;    // 적의 타겟

    public Transform targetPlayer;
    public float distanceToPlayer;
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

        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        Debug.Log($"시작 상태 : {startState}");

        if (startState == EnemyStartState.Idle)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            Debug.Log("대기 상태 진입");
        }
        else if(startState == EnemyStartState.Trace)
        {
            stateMachine.ChangeState(stateMachine.PlayerTargetState);
            Debug.Log("플레이어 타겟 상태 진입");
        }

    }

    private void Update()
    {
        stateMachine.Update();

        if (stateMachine.enemy.navMeshAgent.velocity.y < 0)
        {
            Debug.Log("fall 상태 진입");
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }

        // 점프 상태 진입
        if (stateMachine.enemy.navMeshAgent.velocity.y > 0)
        {
            Debug.Log("jump 상태 진입");
            stateMachine.ChangeState(stateMachine.JumpState);
            return;
        }

    }

    private void OnEnable()
    {
        StartCoroutine(CheckDistanceToPlayer());
    }

    private void OnDisable()
    {
        StopCoroutine(CheckDistanceToPlayer());
    }



    /// <summary>
    /// 플레이어와의 distance를 계산하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckDistanceToPlayer()
    {
        while(true)
        {
            distanceToPlayer = DistanceToPlayer();
            yield return new WaitForSeconds(0.1f);
        }
    }


    /// <summary>
    /// 플레이어와의 distance를 계산하는 함수
    /// </summary>
    /// <returns></returns>
    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, targetPlayer.position);
    }


    /// <summary>
    /// distance를 시야 거리랑 비교해서 시야 거리보다 짧을 경우 해당 플레이어를 향해 ray발사        
    /// </summary>
    public bool CheckTargetInSight()
    {
        if(distanceToPlayer <= detectionRange) // 비교
        {
            RaycastHit hitA;
            // 레이 발사함
            if(Physics.Raycast(transform.position, targetPlayer.position - transform.position, out hitA, detectionRange, targetLayer))
            {
                if (hitA.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
                {
                    return false;
                }

                // a 벡터 계산 시작/ raycast를 솼던 플레이어를 향해 쏜 방향벡터를 가지고 옴
                Vector3 a = targetPlayer.position - transform.position;
                a.y = 0;
                a = a.normalized;

                // 플레이어 방향 벡터 계산
                Vector3 b = transform.forward;
                b.y = 0;
                b = b.normalized;

                float angle = Vector3.Angle(a, b);  // 얘는 0에서 180도 사이의 각도를 반환함
                Debug.Log($"angle : {angle}");
                if (angle <= detectionAngle)   // 만약에 detectionAngle이 30이면, 왼쪽 30 / 오른쪽 30 => 총 60도 만큼 체크해줌
                {
                    Debug.Log("플레이어 타겟 발견");
                    stateMachine.ChangeState(stateMachine.PlayerTargetState);
                    return true;
                }
            }

        }

        return false;
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Initialize()
    {
        curHp = baseHp;
        animator.SetBool("Die", false);
        animator.SetBool("Move", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Skill", false);
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = true;
        navMeshAgent.ResetPath();
        target = null;

        if (startState == EnemyStartState.Idle)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        else if (startState == EnemyStartState.Trace)
        {
            stateMachine.ChangeState(stateMachine.PlayerTargetState);
        }            

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

   
    public void Jump()
    {
        // 점프 처리

    }

}