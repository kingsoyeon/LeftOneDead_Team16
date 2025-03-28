using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyMovementData
{
    [field: SerializeField][field: Range(0, 10)]
    public float MoveSpeed { get; private set; } = 5;   // 기본 이동 스피드

    [field: SerializeField][field: Range(0, 10)]
    public float RotateSpeed { get; private set; } = 1f; // 기본 회전 스피드

    [field: SerializeField][field: Range(0, 10)]
    public float TraceDistance { get; private set; } = 10; // 탐색 거리

}

[System.Serializable]
public class EnemyStatData
{
    [field: SerializeField] public int BaseHp {get; private set;}
    [field: SerializeField] public int BaseAtk {get; private set;}
    [field: SerializeField] public int BaseDef {get; private set;}
}   


[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public EnemyMovementData MovementData { get; private set; }

    [field: SerializeField] public EnemyStatData StatData { get; private set; } 
}






