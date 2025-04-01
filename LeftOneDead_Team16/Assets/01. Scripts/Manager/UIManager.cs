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
    private Stack<PopupUI> popupStack = new Stack<PopupUI>();
    private ScreenUI currentScreen;
    public Transform uirootTransform; // 이 밑으로 UI 생성
    private Dictionary<string, PopupUI> pool = new Dictionary<string, PopupUI>(); // 팝업 UI용 오브젝트풀

    /// <summary>
    /// 고정된 UI를 띄워주는 메서드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T ShowScreen<T> (string name = null) where T : ScreenUI
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        if (currentScreen != null)
        { Destroy(gameObject); }

        GameObject prefab = Resources.Load<GameObject>($"UI/Screen/{name}");
        var obj = Instantiate(prefab, uirootTransform);
        currentScreen = obj.GetComponent<ScreenUI>();
        currentScreen.Show();

        return currentScreen as T;
    }
    /// <summary>
    /// 팝업되는 UI를 띄워주는 메서드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T ShowPopup<T>(string name = null) where T : PopupUI
    {
        PopupUI popup;

        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        if (pool.TryGetValue(name, out popup))
        {
            popup.gameObject.SetActive(true);
        }
        else
        {
            GameObject prefab = Resources.Load<GameObject>($"UI/Popup/{name}");
            var obj = Instantiate(prefab, uirootTransform);
            popup = obj.GetComponent<PopupUI>(); 

            pool[name] = popup;
            popup.Show();
        }

        //popup.Show();
        popupStack.Push(popup);
        return popup as T;
    }
    /// <summary>
    /// 팝업 UI 닫기
    /// </summary>
    public void ClosePopup()
    {
        if (popupStack.Count == 0) return;

        var popup = popupStack.Pop();
        popup.Hide();
    } 
    /// <summary>
    /// 모든 팝업 닫기
    /// </summary>
    public void CloseAllPopup()
    {
        while (popupStack.Count > 0)
        {
            ClosePopup();
        }
    }
}
