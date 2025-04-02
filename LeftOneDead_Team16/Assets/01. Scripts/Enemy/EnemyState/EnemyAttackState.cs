using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float lastAttackTime; // 일반 공격 마지막 시간
    private float lastSkillAttackTime;  // 스킬 공격 마지막 시간
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.enemy.navMeshAgent.isStopped = true;
        stateMachine.enemy.animator.SetBool("Move", false);

        // 들어오면 일단 처음 공격함
        lastAttackTime = Time.time - stateMachine.enemy.attackSpeed;
        Attack(); 

    }

    private float allTime = 0f;
    public override void Update()
    {
        base.Update();
        allTime += Time.deltaTime;  

        if(allTime >= 0.1f)
        {
            CheckPlayerDistance();

            // 어택 공격할 때 플레이어가 돌아갈 수 있으니 체크
            if (stateMachine.enemy.target != null)
            {
                stateMachine.enemy.target.transform.Rotate(Vector3.up, stateMachine.enemy.rotateSpeed * Time.deltaTime);
            }
            
            allTime = 0f;
        }



        UseSkillOrAttack(); // 스킬 공격 또는 일반 공격 처리
    }
    public override void Exit()
    {
        base.Exit();
        stateMachine.enemy.navMeshAgent.isStopped = false;
    }


    /// <summary>   
    /// 스킬 공격 또는 일반 공격 처리
    /// 스킬 공격 쓸 수 있으면 우선적으로 스킬 사용
    /// 스킬 공격 쓸 수 없으면 일반 공격
    /// </summary>

    public void UseSkillOrAttack()
    {
        float currentTime = Time.time;
        if(stateMachine.enemy.skill != null && currentTime - lastSkillAttackTime >= stateMachine.enemy.skillSpeed)
        {
            SkillAttack();
            lastAttackTime = currentTime;
        }
        else if(currentTime - lastAttackTime >= stateMachine.enemy.attackSpeed)
        {
            Attack();
            lastAttackTime = currentTime;
        }

    }


    /// <summary>
    /// 스킬 공격 처리  
    /// </summary>
    public void SkillAttack()
    {
        float skillSpeed = stateMachine.enemy.skillSpeed;
        if(Time.time - lastSkillAttackTime >= skillSpeed)
        {
            lastSkillAttackTime = Time.time;
            stateMachine.enemy.animator.SetTrigger("Skill");
            stateMachine.enemy.skill?.UseSkill();


        }
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