using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillAttackState : EnemyBaseState
{
    public EnemySkillAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.enemy.animator.SetTrigger("Skill");
    }
    public override void Exit()
    {
        base.Exit();
    }
}