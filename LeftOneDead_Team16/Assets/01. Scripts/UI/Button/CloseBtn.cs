using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        UIManager.Instance.CloseAllPopup();
    }
}
