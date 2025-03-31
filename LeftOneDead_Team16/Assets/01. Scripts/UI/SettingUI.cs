using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : PopupUI
{
    public void OnBackButtonClicked()
    {
        UIManager.Instance.ClosePopup();
    }
}
