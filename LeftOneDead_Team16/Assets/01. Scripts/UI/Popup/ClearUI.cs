using DG.Tweening;
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
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f; 

   
    public void OnSkipButtonClicked()
    {
        GameManager.Instance.ChangeToLobby();
    }

    private void Start()
    {
        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            killCountText.alpha = 0;
            playTimeText.alpha = 0;

            killCountText.DOFade(1f, 0.5f);
            playTimeText.DOFade(1f, 0.5f);

            killCountText.text = $"처치 횟수 : {StageManager.Instance.KillCount}";
            int minutes = (int)(StageManager.Instance.PlayTime / 60);
            int seconds = (int)(StageManager.Instance.PlayTime % 60);
            playTimeText.text = $"클리어 시간 : {minutes}분 {seconds}초";
        });
    }
}
