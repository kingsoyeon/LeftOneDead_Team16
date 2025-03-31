using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "SO/StageData")]
public class StageData : ScriptableObject
{
    public int chapter;
    public int stage;

    public Vector3 playerRespawnPos;
    
    public List<Vector3> enemyInitialRespawnPosList;    // 일반 몬스터 초기 리스폰 위치

    public List<GameObject> enemyResourceList;  // 일반 몬스터 프리팹 리소스
}
