using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임에서
/// Scroll/ESC 처리
/// </summary>
public class PlayerInputHandler : IInputHandler
{
    /// <summary>
    /// 게임 중 ESC를 눌렀을 때 UI 팝업이 뜨고, UI 상태로 변경 
    /// </summary>
    public void OnEscPressed()
    {
        UIManager.Instance.ShowPopup<SettingUI>("SettingUI");
        InputManager.Instance.ChangeToUIActions();
    }

    public void OnScroll()
    {
        // 구현 예정
    }
}
