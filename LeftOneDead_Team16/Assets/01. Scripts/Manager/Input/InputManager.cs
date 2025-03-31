using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 게임 내 플레이어 조작을 제외한 Input 관리
/// (StageScene에만 존재해야 합니다)
/// </summary>
public class InputManager : Singleton<InputManager>
{
    public Inputs inputs { get; private set; }
    public Inputs.PlayerActions playerActions { get; private set; }
    public Inputs.UIActions uiActions { get; private set; }

    public IInputHandler inputHandler;
    public PlayerInputHandler playerInputHandler = new();
    public UIInputHandler uiInputHandler = new();

    protected override void Awake()
    {
        base.Awake();

        inputs = new Inputs();
        playerActions = inputs.Player;
        uiActions = inputs.UI;

        ChangeToPlayerActions(); // 초기상태

        playerActions.Swap.performed += OnSwapPerformed; // swap-스크롤
        playerActions.Pause.started += OnPausePerformed; // Esc 눌렀을 때, UI 팝업 (settingUI)
        uiActions.Close.started += OnPausePerformed; // 켜져있을 때 Esc 누르면 UI 팝업 꺼짐
    }

    private void OnSwapPerformed(InputAction.CallbackContext context)
    {
        // 스크롤 구현 예정
    }
    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        inputHandler.OnEscPressed();
    }

    /// <summary>
    /// 플레이어 액션맵으로 변경 (인게임에서 UI 끄고나서)
    /// </summary>
    public void ChangeToPlayerActions()
    {
        inputs.Player.Enable();
        inputs.UI.Disable();
        inputHandler = playerInputHandler;
    }

    /// <summary>
    /// UI 액션맵으로 변경 (인게임에서 UI 띄울 때)
    /// </summary>
    public void ChangeToUIActions()
    {
        inputs.Player.Disable();
        inputs.UI.Enable();
        inputHandler = uiInputHandler;
    }
}
