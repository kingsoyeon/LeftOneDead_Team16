using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageSelectUI : PopupUI
{
    [SerializeField] private List<StageData> stageList;


    private void Start()
    {

    }


    /// <summary>
    /// 스테이지 데이터 가져오기
    /// </summary>
    public void LoadStage()
    {
        stageList = Resources.LoadAll<StageData>("StageData").ToList();
    }

    public void OnStageSelectButton()
    {
        GameManager.Instance.StageStart();
    }
}
