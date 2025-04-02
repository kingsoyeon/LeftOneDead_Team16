using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : PopupUI
{
    public void OnClicked()
    {
        Time.timeScale = 1;
        GameManager.Instance.ChangeToLobby();
    }
}
    

