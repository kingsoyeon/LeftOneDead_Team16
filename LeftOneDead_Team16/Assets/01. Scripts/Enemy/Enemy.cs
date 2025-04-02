using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStartState
{
    Idle,
    Trace,
    
}


public class Enemy : MonoBehaviour, IDamageable
{

    public AudioClip zombieSound;
    [field:SerializeField] private EnemySO enemySO;
    [field:SerializeField] private EnemyStateMachine stateMachine;
    [field:SerializeField] private EnemyStateMachine beforeStateMachine;
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
    public float traceRange => enemySO.MovementData.PatrolRange;
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
    public bool isLive = true;

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

        if (startState == EnemyStartState.Idle)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        else if(startState == EnemyStartState.Trace)
        {
            stateMachine.ChangeState(stateMachine.PlayerTargetState);
        }

    }


    private void Update()
    {
        stateMachine.Update();

        // 에이전트가 isOnOffMeshLink에 있는지 확인한다. (어차피 nav로 움직이니까 저걸로 떨이지고 올라갈거임)
        if (stateMachine.enemy.navMeshAgent.isOnOffMeshLink && !stateMachine.enemy.navMeshAgent.isStopped)
        {
            OffMeshLinkData linkData = stateMachine.enemy.navMeshAgent.currentOffMeshLinkData;
            if(linkData.linkType == OffMeshLinkType.LinkTypeJumpAcross) // jumpAcross 타입이면 점프 상태로 변경
            {
                stateMachine.ChangeState(stateMachine.JumpState);

                return;
            }
            else if(linkData.linkType == OffMeshLinkType.LinkTypeDropDown) // dropDown 타입이면 떨어지는 상태로 변경
            {
                stateMachine.ChangeState(stateMachine.FallState);
                return;
            }
            else if(linkData.offMeshLink != null && linkData.offMeshLink.area == 3) // climb 타입이면 벽을 오르는 상태로 변경
            {
                stateMachine.ChangeState(stateMachine.ClimbState);
                return;
            }
        }

        // if (stateMachine.enemy.navMeshAgent.velocity.y < -1f && !stateMachine.enemy.navMeshAgent.isStopped)
        // {
        //     Debug.Log("fall 상태 진입");
        //     stateMachine.ChangeState(stateMachine.FallState);
        //     return;
        // }

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
                if (angle <= detectionAngle)   // 만약에 detectionAngle이 30이면, 왼쪽 30 / 오른쪽 30 => 총 60도 만큼 체크해줌
                {
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
        animator.SetBool("Death", false);
        animator.SetBool("Move", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Skill", false);
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = true;
        navMeshAgent.ResetPath();
        target = null;
        isLive = true;

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
        SoundManager.PlayClip(zombieSound);

        float damageMultiplier = 100f / (100f + baseDef);
        // 데미지 받기 방어력 적용해서 데미지 계산
        damage = Mathf.Max(Mathf.RoundToInt(damage * damageMultiplier), 1);
       
        // 데미지 처리
        curHp -= damage;
        // Debug.Log(curHp);

        // 체력이 0이하면 죽음
        if(curHp <= 0 && isLive)
        {
            Die();
            
        }
    }   

    

    private void Die()
    {
        animator.SetBool("Death", true);
        isLive = false;
        StartCoroutine(DieCorutine());
        
    }

    IEnumerator DieCorutine()
    {
        navMeshAgent.isStopped = true;

        float duration = 3f;
        float elapsed = 0f;
        float startY = transform.position.y;
        float targetY = startY - 1.0f; 

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newY = Mathf.Lerp(startY, targetY, elapsed / duration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        // 최종 위치 보정
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        Destroy(gameObject);
    }


    public void StartJump()
    {
        StartCoroutine(Jump());
    }

   /// <summary>
   /// 점프하는 코루틴
   /// 포물선으로 점프        
   /// </summary>
   /// <returns></returns>
    IEnumerator Jump()
    {
        navMeshAgent.isStopped = true;
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = linkData.endPos;

        float jumpTime = 1.0f; // 원하는 점프 시간 (조정 가능)
        float g = 9.8f;       // 중력 가속도
                              // 초기 속도 계산: (endY - startY + 0.5 * g * jumpTime^2) / jumpTime
        float v0 = ((endPosition.y - startPosition.y) + 0.5f * g * jumpTime * jumpTime) / jumpTime;

        float elapsedTime = 0f;
        while (elapsedTime < jumpTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsedTime, 0, jumpTime);
            float percent = t / jumpTime;

            // 수평 위치 보간 (선형 보간)
            Vector3 horizontalPos = Vector3.Lerp(startPosition, endPosition, percent);

            // 수직 위치: y = startY + v0 * t - 0.5 * g * t^2
            float yOffset = v0 * t - 0.5f * g * t * t;
            horizontalPos.y = startPosition.y + yOffset;

            transform.position = horizontalPos;
            yield return null;
        }

        // 착지 보정
        transform.position = endPosition;
        navMeshAgent.CompleteOffMeshLink();
        navMeshAgent.isStopped = false;
    }


    public void StartFall()
    {
        StartCoroutine(Fall());
    }

    /// <summary>
    /// 떨어지는 코루틴
    /// 중력기반 낙하 
    /// </summary>
    /// <returns></returns>
    IEnumerator Fall()
    {
        // 떨어지는 동안 NavMeshAgent를 정지
        navMeshAgent.isStopped = true;

        // 현재 OffMeshLink 정보를 가져옴
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = linkData.endPos;

        float g = 9.8f;  // 중력 가속도
                         // 떨어지는 높이 계산 (startPosition.y > endPosition.y 가정)
        float height = startPosition.y - endPosition.y;
        // 자유 낙하 공식: t = sqrt(2 * height / g)
        float fallTime = Mathf.Sqrt(2 * height / g);

        float elapsedTime = 0f;
        while (elapsedTime < fallTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsedTime, 0, fallTime);

            // 수직 위치: y = startY - 0.5 * g * t^2
            float newY = startPosition.y - 0.5f * g * t * t;
            newY = Mathf.Max(newY, endPosition.y);

            // 수평 보간 (선형 보간)
            float percent = t / fallTime;
            Vector3 horizontalPos = Vector3.Lerp(new Vector3(startPosition.x, 0, startPosition.z),
                                                  new Vector3(endPosition.x, 0, endPosition.z), percent);

            transform.position = new Vector3(horizontalPos.x, newY, horizontalPos.z);
            yield return null;
        }

        // 최종 위치 보정 및 OffMeshLink 완료 처리
        transform.position = endPosition;
        navMeshAgent.CompleteOffMeshLink();
        navMeshAgent.isStopped = false;
    }

    public void StartClimb()
    {
        StartCoroutine(Climb());
    }

    /// <summary>
    /// 벽을 오르는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Climb()
    {
        // NavMeshAgent의 자동 이동을 중지합니다.
        navMeshAgent.isStopped = true;

        // 현재 OffMeshLink 데이터를 가져옵니다.
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = linkData.endPos;

        // 벽을 오르는 속도: 이동 속도의 50% (필요에 따라 조정)
        float climbSpeed = moveSpeed * 0.5f;
        // 시작점과 끝점 사이의 거리를 구합니다.
        float distance = Vector3.Distance(startPosition, endPosition);
        // 전체 클라임 시간 = 거리 / 속도
        float climbTime = distance / climbSpeed;

        float elapsedTime = 0f;
        while (elapsedTime < climbTime)
        {
            elapsedTime += Time.deltaTime;
            // 0~1 사이의 진행률
            float t = Mathf.Clamp01(elapsedTime / climbTime);
            // 선형 보간으로 위치 업데이트
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);
            transform.position = newPosition;
            yield return null;
        }

        // 최종 위치 보정: 정확히 종료 위치로 이동
        transform.position = endPosition;

        // OffMeshLink 완료 처리 및 NavMeshAgent 재개
        navMeshAgent.CompleteOffMeshLink();
        navMeshAgent.isStopped = false;
    }

    /// <summary>
    /// 벽을 오르는 링크인지 확인하는 함수  
    /// </summary>
    /// <returns></returns>
    public bool IsClimbLink()
    {
        if(navMeshAgent.isOnOffMeshLink)
        {

            OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
            if(linkData.offMeshLink!=null && linkData.offMeshLink.area == 3)
            {
                return true;
            }
        }
        return false;
    }

}