using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyPatrolState : EnemyBaseState
{

    [Header("Wandering")]
    private float minWanderDistance = 3f;
    private float maxWanderDistance = 5f;
    private float minWanderWaitTime = 1f;
    private float maxWanderWaitTime = 3f;

    Vector3 patrolPosition;

    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PatrolState");
        patrolPosition = GetPatrolPosition();
        stateMachine.enemy.navMeshAgent.SetDestination(patrolPosition);
    }

    public override void Update()
    {
        base.Update();
        if(stateMachine.enemy.FindNearestTarget()) return;
        Patrol();
    }

    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 정찰 함수
    /// </summary>
    private void Patrol()
    {
        // 정찰 위치에 도달했을 때 아이들 상태로 전환
        if(Vector3.Distance(stateMachine.enemy.transform.position, patrolPosition) < 0.1f)
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

        NavMesh.SamplePosition(stateMachine.enemy.transform.position +
       (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0; 
        // 정찰 위치가 길어질 때까지 반복(최대30회)
        while(Vector3.Distance(stateMachine.enemy.transform.position, hit.position) <stateMachine.enemy.traceRange)
        {
            NavMesh.SamplePosition(stateMachine.enemy.transform.position + 
            (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if(i==30) break;
        }
        return hit.position;
    }

}