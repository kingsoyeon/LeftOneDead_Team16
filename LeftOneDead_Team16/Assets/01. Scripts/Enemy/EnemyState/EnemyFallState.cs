using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallState : EnemyBaseState
{
    public EnemyFallState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.enemy.animator.SetBool("Fall", true);
    }
    public override void Exit()
    {
        base.Exit();
        stateMachine.enemy.animator.SetBool("Fall", false);
    }

    public override void Update()
    {
        base.Update();
       if (stateMachine.enemy.navMeshAgent.isOnNavMesh)
       {
        if(stateMachine.enemy.targetPlayer != null)
        {
            stateMachine.ChangeState(stateMachine.PlayerTargetState);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.PatrolState);
        }
       }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
      
    }
}
