using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 개별 UI의 추상 클래스
/// </summary>
public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;
    public virtual void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }
}
