using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerTargetState : EnemyBaseState
{
    public EnemyPlayerTargetState(EnemyStateMachine stateMachine) : base(stateMachine)  
    {
    }

    public override void Enter()
    {
        Debug.Log("플레이어 타겟 상태 진입");

        base.Enter();
        SetTarget();
        stateMachine.enemy.navMeshAgent.isStopped = false;
        stateMachine.enemy.navMeshAgent.speed = stateMachine.enemy.runSpeed;
        // 플레이어 타겟 추적   
        TrackPlayer();

        // 애니메이션 설정
        stateMachine.enemy.animator.SetBool("Move", true);
        stateMachine.enemy.animator.SetFloat("Speed", 1f);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.enemy.navMeshAgent.speed = stateMachine.enemy.moveSpeed;
    }

    public override void Update()
    {
        base.Update();
        if(IsCloseToPlayer())
        {
            stateMachine.enemy.navMeshAgent.SetDestination(stateMachine.enemy.transform.position);
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    /// <summary>
    /// 플레이어 타겟 설정
    /// </summary>
    private void SetTarget()
    {
        // 플레이어 타겟 찾기 일단 tag로 찾음 => instance 찾는걸로 수정하면 좋을듯
        stateMachine.enemy.target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log($"플레이어 타겟 찾기 : {stateMachine.enemy.target}");
    }

    /// <summary>
    /// 플레이어 타겟 추적
    /// </summary>
    private void TrackPlayer()
    {
        if(stateMachine.enemy.target != null)
        {
            stateMachine.enemy.MoveToPosition(stateMachine.enemy.target.position);
        }
    }


    // 플레이어와 가까워졌는지 체크
    private bool IsCloseToPlayer()
    {
        return Vector3.Distance(stateMachine.enemy.transform.position, stateMachine.enemy.target.position) < stateMachine.enemy.attackRange;
    }
}