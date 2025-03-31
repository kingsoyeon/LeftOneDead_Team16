using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyScene : MonoBehaviour
{
    private void Start()
    {
        //GameManager.Instance.ChangeState(GameState.Lobby);
        UIManager.Instance.ShowScreen<LobbyUI>("LobbyUI");
    }
}
