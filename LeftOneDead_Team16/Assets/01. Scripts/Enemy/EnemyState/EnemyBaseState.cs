using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
    }
    public virtual void FixedUpdate()
    {
    }

    /// <summary>
    /// 타겟 위치로 움직임      
    /// </summary>
    /// <param name="targetPos">타겟 위치</param>
    public void MoveTo(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - stateMachine.enemy.transform.position).normalized;
        Move(dir);
    }


    /// <summary>
    /// 캐릭터 컨트롤러를 통해 움직임
    /// </summary>
    /// <param name="dir">움직이는 방향</param>
    public void Move(Vector3 dir)
    {
        // 방향으로 회전
        Rotate(dir);
        // 방향으로 움직임
        Vector3 moveDirection = dir * stateMachine.enemy.moveSpeed * Time.deltaTime;
        stateMachine.enemy.characterController.Move(moveDirection);
    }

    /// <summary>
    /// 외부 힘에 의한 이동
    /// </summary>
    /// <param name="force">외부 힘</param>
    public void ForeceMove(Vector3 force)
    {
        stateMachine.enemy.characterController.Move(force * Time.deltaTime);
    }

    /// <summary>
    /// 방향으로 회전   
    /// </summary>
    /// <param name="movementDirection">회전 방향</param>
    public void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.enemy.transform.rotation = Quaternion.Lerp(stateMachine.enemy.transform.rotation, targetRotation, stateMachine.enemy.rotateSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 플레이어와의 거리 체크해서 탐지
    /// <param name="detectionRange">탐지 거리</param>
    /// <param name="target">타겟</param>
    public void CheckForPlayer(float detectionRange, Transform target)
    {
        if (Vector3.Distance(stateMachine.enemy.transform.position, target.position) <= detectionRange)
        {
            stateMachine.ChangeState(stateMachine.TraceState);
        }
    }

}
