using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Stage curStage; // 현재 스테이지

    private Dictionary<int, Action> eventActionDict; // 이벤트 저장 딕셔너리

    public Player Player { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        eventActionDict = new Dictionary<int, Action>();
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