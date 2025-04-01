using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "SO/StageData")]
public class StageData : ScriptableObject
{
    public int chapter;
    public int stage;
    public string stageName;

    public SceneAsset scene;
}
