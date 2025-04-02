using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : ScreenUI
{
   public void OnStartButtonClicked()
    {
        UIManager.Instance.ShowPopup<ChapterSelectUI>("ChapterSelectUI");
    }

    public void OnSettingButtonClicked()
    {
        UIManager.Instance.ShowPopup<SettingUI>("SettingUI");
        UIManager.Instance.ShowPopup<GeneralSettingUI>("GeneralSettingUI");
    }
}
