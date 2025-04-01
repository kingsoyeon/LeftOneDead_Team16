using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageScene : MonoBehaviour
{
    private void Start()
    {
        //GameManager.Instance.ChangeState(GameState.Game);
        UIManager.Instance.ShowScreen<InGameUI>("InGameUI");
        UIManager.Instance.ShowScreen<HPbar>("HPbar");

    }
}
