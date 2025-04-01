using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// 버튼과 StageData(SO)와 연결
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    private StageData stageData;
    [SerializeField] private TextMeshProUGUI stageNameText;

    public void Init(StageData data)
    {
        stageData = data;
        stageNameText.text = data.stageName;
    }

    public void OnStageSelectButton()
    {
        GameManager.Instance.StageStart(stageData);
    }
}
