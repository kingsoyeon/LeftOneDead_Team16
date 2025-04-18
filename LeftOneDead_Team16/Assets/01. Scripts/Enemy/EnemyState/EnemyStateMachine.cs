using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy enemy { get; private set; }

    public EnemyIdleState IdleState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyTraceState TraceState { get; private set; }
    public EnemyPlayerTargetState PlayerTargetState { get; private set; }
    public EnemySkillAttackState SkillAttackState { get; private set; }
    public EnemyJumpState JumpState { get; private set; }
    public EnemyFallState FallState { get; private set; }
    public EnemyClimbState ClimbState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        this.enemy = enemy;

        IdleState = new EnemyIdleState(this);
        AttackState = new EnemyAttackState(this);
        PatrolState = new EnemyPatrolState(this);
        TraceState = new EnemyTraceState(this);
        SkillAttackState = new EnemySkillAttackState(this);
        PlayerTargetState = new EnemyPlayerTargetState(this);
        JumpState = new EnemyJumpState(this);
        FallState = new EnemyFallState(this);
        ClimbState = new EnemyClimbState(this);
    }



}
