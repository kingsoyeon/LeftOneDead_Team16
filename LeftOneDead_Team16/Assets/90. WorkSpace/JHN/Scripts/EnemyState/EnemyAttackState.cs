using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float lastAttackTime;
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Update()
    {
        base.Update();
        CheckPlayerDistance();
        Attack();
    }
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 공격 처리
    /// </summary>
    public void Attack()
    {
        float currentTime = Time.time;
        if(currentTime - lastAttackTime >= stateMachine.enemy.attackSpeed)
        {
            lastAttackTime = currentTime;
            stateMachine.enemy.animator.SetTrigger("Attack");

            // 나중에 플레이어 생기면 플레이어의 takeDamage 호출
            stateMachine.enemy.target.TryGetComponent<IDamageable>(out IDamageable damageable);
            if(damageable != null)
            {
                damageable.TakeDamage(stateMachine.enemy.baseAtk);
            }

            Debug.Log("Attack");
        }
    }

    // 플레이어가 멀어지면 공격상태 종료 후 trace 상태로 전환
    private void CheckPlayerDistance()
    {
        if(Vector3.Distance(stateMachine.enemy.transform.position, stateMachine.enemy.target.position) > stateMachine.enemy.attackRange)
        {
            stateMachine.ChangeState(stateMachine.TraceState);
        }
    }
    
}