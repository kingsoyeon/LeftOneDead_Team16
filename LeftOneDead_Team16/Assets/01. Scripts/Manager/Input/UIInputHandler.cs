using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임 내 UI 상태에서 로직 관리 
/// </summary>
public class UIInputHandler : IInputHandler
{
    /// <summary>
    /// UI 상태에서 ESC 눌렀을 때, 팝업이 꺼지는 메소드
    /// </summary>
    public void OnEscPressed()
    {
        UIManager.Instance.ClosePopup();
        InputManager.Instance.ChangeToPlayerActions();
    }

    public void OnScroll()
    {
        // 사용안함
    }
}
