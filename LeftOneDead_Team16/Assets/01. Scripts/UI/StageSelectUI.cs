using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StageSelectUI : PopupUI
{
    [SerializeField] private List<StageData> stageList;
    [SerializeField] private GameObject stageButtonPrefab;
    [SerializeField] private Transform parentTransform;
    


    private void Start()
    {
        LoadStage();
        foreach (var stage in stageList) 
        {
            var stageButton = Instantiate(stageButtonPrefab, parentTransform);
            stageButton.SetActive(true);
            var button = stageButton.GetComponent<StageSelectButton>();
            button.Init(stage);
        }
    }

    /// <summary>
    /// 스테이지 데이터 가져오기
    /// </summary>
    public void LoadStage()
    {
        stageList = Resources.LoadAll<StageData>("Data/SO/Stage").ToList();
    }

    public void OnStageSelectButton()
    {
        // 현재 강제적으로 스테이지 1-1에 이동 중 - 임시
        GameManager.Instance.StageStart();
    }
}
