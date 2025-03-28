using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI 상태
/// </summary>
public enum UIState
{
    Lobby,
    Setting,
    Loading,
    InGame
}
/// <summary>
/// UI간의 전환
/// </summary>
public class UIManager : Singleton<UIManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
}
