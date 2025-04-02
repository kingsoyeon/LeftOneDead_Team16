using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : PopupUI
{
    public void Start()
    {
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(5f);
        GameManager.Instance.ChangeToLobby();
        Time.timeScale = 1;
    }
    //public void OnClicked()
    //{
    //    Time.timeScale = 1;
    //    GameManager.Instance.ChangeToLobby();
    //}
}
    

