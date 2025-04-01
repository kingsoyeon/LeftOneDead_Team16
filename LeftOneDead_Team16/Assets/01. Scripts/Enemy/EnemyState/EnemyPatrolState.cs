using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyPatrolState : EnemyBaseState
{

    [Header("Wandering")]
    private float maxWanderDistance = 5f;
    private float maxDistance = 5f;


    private float patrolMaxTime = 5f;
    private float patrolTime = 0f;


    Vector3 patrolPosition;

    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PatrolState");
        stateMachine.enemy.navMeshAgent.isStopped = false;
        stateMachine.enemy.animator.SetBool("Move", true);
        patrolPosition = GetPatrolPosition();
        stateMachine.enemy.navMeshAgent.SetDestination(patrolPosition);
        stateMachine.enemy.animator.SetFloat("Speed", 0f);
        patrolTime = 0f;
        allTime = 0f;
    }

    float allTime = 0f;
    public override void Update()
    {
        base.Update();

        allTime += Time.deltaTime;
        if(allTime > 0.1f){

            if(stateMachine.enemy.CheckTargetInSight()) return;
            Patrol();
            CheckPatrolTime();
            allTime = 0f;
        }
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.enemy.animator.SetBool("Move", false); 
        stateMachine.enemy.animator.SetFloat("Speed", 1f);
    }

    /// <summary>
    /// 정찰 함수
    /// </summary>
    private void Patrol()
    {
        Debug.Log("Patrol");
        Debug.Log($"patrolPosition: {patrolPosition}");
        patrolTime += Time.deltaTime;

        // 정찰 위치에 도달했을 때 아이들 상태로 전환
        if (Vector3.Distance(stateMachine.enemy.transform.position, patrolPosition) < 0.1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    /// <summary>
    /// 정찰할 위치를 가져오는 함수
    /// </summary>
    /// <returns>정찰 위치</returns>
    private Vector3 GetPatrolPosition()
    {
        Debug.Log("GetPatrolPosition");
        NavMeshHit hit;

        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(stateMachine.enemy.traceRange, maxWanderDistance);
        Vector3 randomOffset = new Vector3(randomCircle.x, 0, randomCircle.y);
        

        if (!NavMesh.SamplePosition(stateMachine.enemy.transform.position + randomOffset, out hit, maxDistance, NavMesh.AllAreas))
        {
            // 실패하면 기본값 반환
            return stateMachine.enemy.transform.position;
        }

        int i = 0; 
        // 정찰 위치가 길어질 때까지 반복(최대60회)
        while(Vector3.Distance(stateMachine.enemy.transform.position, hit.position) < stateMachine.enemy.traceRange)
        {
            i++;
            if(i==60) 
            {
                return stateMachine.enemy.transform.position;
            }

            randomCircle = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(stateMachine.enemy.traceRange, maxWanderDistance);
            randomOffset = new Vector3(randomCircle.x, 0, randomCircle.y);

            NavMesh.SamplePosition(stateMachine.enemy.transform.position + randomOffset, out hit, maxDistance, NavMesh.AllAreas);

        }
        return hit.position;
    }

    // 일정 시간 patrol을 반복했다면 꼈다고 판단하고 대기 상태로 전환
    private void CheckPatrolTime()
    {
        if(patrolTime > patrolMaxTime)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}