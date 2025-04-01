using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 게임 클리어 조건을 만족했을 때 뜨는 팝업
/// </summary>
public class ClearUI : PopupUI
{
   public void OnSkipButtonClicked()
    {
        GameManager.Instance.ChangeToLobby();
    }
}
