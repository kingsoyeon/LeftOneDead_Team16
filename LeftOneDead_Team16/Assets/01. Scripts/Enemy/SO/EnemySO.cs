using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyMovementData
{
    [field: SerializeField][field: Range(0, 20)]
    public float MoveSpeed { get; private set; } = 5;   // 기본 이동 스피드

    [field: SerializeField][field: Range(0, 40)]
    public float RunSpeed { get; private set; } = 10;   // 뛰기 스피드

    [field: SerializeField][field: Range(0, 10)]
    public float JumpForce { get; private set; } = 10; // 점프 힘

    [field: SerializeField][field: Range(0, 10)]
    public float RotateSpeed { get; private set; } = 1f; // 기본 회전 스피드

    [field: SerializeField][field: Range(0, 30)]
    public float DetectionRange { get; private set; } = 10; // 감지 거리

    [field: SerializeField][field: Range(0, 60)]
    public float DetectionAngle { get; private set; } = 30; // 감지 각도

    [field: SerializeField][field: Range(0, 10)]
    public float PatrolRange { get; private set; } = 10; // 순찰 거리


    [field: SerializeField][field: Range(0, 10)]
    public float PatrolTime { get; private set; } = 5; // 순찰 시간

    [field: SerializeField][field: Range(0, 10)]
    public float PatrolWaitTime { get; private set; } = 1; // 순찰 대기 시간
    [field: SerializeField][field: Range(0, 10)]
    public float AttackRange { get; private set; } = 2; // 공격 거리
    
    [field: SerializeField][field: Range(0, 10)]
    public float AttackSpeed { get; private set; } = 1; // 공격 속도

    [field: SerializeField][field: Range(0, 10)]
    public float SkillAttackRange { get; private set; } = 5; // 스킬 공격 거리

    [field: SerializeField][field: Range(0, 10)]
    public float SkillAttackSpeed { get; private set; } = 1; // 스킬 공격 속도

}

[System.Serializable]
public class EnemyStatData
{
    [field: SerializeField] public int BaseHp {get; private set;}
    [field: SerializeField] public int BaseAtk {get; private set;}
    [field: SerializeField] public int BaseDef {get; private set;}
}   

public enum EnemyType
{
    Regular,
    VomitZombie,

}

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public EnemyType enemyType { get; private set; }
    [field: SerializeField] public EnemyMovementData MovementData { get; private set; }

    [field: SerializeField] public EnemyStatData StatData { get; private set; } 
}






