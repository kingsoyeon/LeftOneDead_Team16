using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Dictionary<int, List<StageData>> stageDataDict;

    private Stage curStage;
    //private Player player;
    
    protected override void Awake()
    {
        base.Awake();
        InitStageInfoData();
    }

    /// <summary>
    /// 스테이지 SO 딕셔너리에 저장
    /// </summary>
    private void InitStageInfoData()
    {
        var stageSOAsset = Resources.LoadAll<StageData>("Data/SO");
        
        if (stageSOAsset != null)
        {
            for (var i = 0; i < stageSOAsset.Length; i++)
            {
                if (stageDataDict.ContainsKey(stageSOAsset[i].chapter))
                {
                    stageDataDict[stageSOAsset[i].chapter].Add(stageSOAsset[i]);
                }
                else
                {
                    var stageList = new List<StageData>();
                    stageList.Add(stageSOAsset[i]);
                    stageDataDict.Add(stageSOAsset[i].chapter, stageList);
                }
            }
        }
    }

    private void InitStage(StageData stageData)
    {
        var stage = new Stage(stageData);
    }
}
