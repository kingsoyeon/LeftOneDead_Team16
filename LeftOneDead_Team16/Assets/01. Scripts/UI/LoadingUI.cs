using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Linq;

public class LoadingUI : ScreenUI
{
    private StageData stageData;

    public CanvasGroup canvasGroup;
    public Image progressBar;
    public Image stageImage; // 스테이지 이미지
    //public TextMeshProUGUI tipText; // 랜덤 팁 
    [SerializeField] private TextMeshProUGUI stageNameText;
    //public TextMeshProUGUI stageInfoText; 스테이지 정보
    // public 

    public void Start()
    {

        stageData = SceneLoader.CurrentStageData;
        Init(stageData);
    }

    public void Init(StageData data)
    {
        stageData = data;
        stageNameText.text = data.stageName;
    }
}
