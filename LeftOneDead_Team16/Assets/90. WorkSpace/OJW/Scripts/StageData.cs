using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "SO/StageData")]
public class StageData : ScriptableObject
{
    public int chapter;
    public int stage;
    public List<Vector3> enemyInitialRespawnPosList;

    public List<GameObject> enemyResourceList;
}
