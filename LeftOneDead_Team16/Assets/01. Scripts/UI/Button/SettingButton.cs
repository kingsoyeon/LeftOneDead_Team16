using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public void OnButtonClicked()
    {
        
        string name = gameObject.name.Replace("Btn", "");

        UIManager.Instance.ShowPopUp($"{name}UI");
    }
}
