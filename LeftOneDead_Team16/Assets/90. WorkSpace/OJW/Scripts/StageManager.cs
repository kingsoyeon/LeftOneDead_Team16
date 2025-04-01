using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Dictionary<int, List<StageData>> stageDataDict;
    private Stage curStage;

    private Dictionary<int, Action> eventActionDict;

    [NonSerialized] public Player Player;

    protected override void Awake()
    {
        base.Awake();
        eventActionDict = new Dictionary<int, Action>();
        InitStageInfoData();
    }

    private void Update()
    {
        // test 코드
        if (Input.GetKeyDown(KeyCode.Space))
        {
            eventActionDict[0]?.Invoke();
        }
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

    public void SetCurrentStage(Stage stage)
    {
        curStage = stage;
        Player = stage.Player;
    }

    public void AddActionToEventActionDict(int index, Action action)
    {
        if (eventActionDict.ContainsKey(index))
        {
            eventActionDict[index] += action;
        }
        else
        {
            eventActionDict.Add(index, action);
        }
    }

    public void ClearStage()
    {
        // 스테이지 클리어
        print("스테이지 클리어");
    }
}