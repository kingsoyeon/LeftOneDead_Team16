using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Stage curStage; // 현재 스테이지

    private Dictionary<int, Action> eventActionDict; // 이벤트 저장 딕셔너리
    private bool isStageEnd;

    public Player Player { get; private set; }
    public float PlayTime { get; private set; }
    public int KillCount { get; set; }

    protected override void Awake()
    {
        base.Awake();
        eventActionDict = new Dictionary<int, Action>();
        isStageEnd = false;
        KillCount = 0;
        PlayTime = 0f;
    }

    private void Update()
    {
        if (isStageEnd) return;
        
        PlayTime += Time.unscaledDeltaTime;
    }

    public void SetCurrentStage(Stage stage)
    {
        curStage = stage;
        Player = stage.Player;
    }

    public void AddActionToEventActionDict(int index, Action action)
    {
        eventActionDict ??= new Dictionary<int, Action>();

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
        isStageEnd = true;
        GameManager.Instance.SetGameState(GameManager.GameState.GameClear);
        UIManager.Instance.ShowPopup<ClearUI>("ClearUI");
    }

    public void InitEventAction(int eventID)
    {
        eventActionDict[eventID]?.Invoke();
    }
}