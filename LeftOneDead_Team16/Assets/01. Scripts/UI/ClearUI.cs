using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 게임 클리어 조건을 만족했을 때 뜨는 팝업
/// </summary>
public class ClearUI : PopupUI
{
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private TextMeshProUGUI killCountText;

    public void OnSkipButtonClicked()
    {
        GameManager.Instance.ChangeToLobby();
    }

    private void Start()
    {
        killCountText.text = $"처치 횟수 : {StageManager.Instance.KillCount}";
        playTimeText.text = $"클리어 시간 : {StageManager.Instance.PlayTime}";
    }
}
