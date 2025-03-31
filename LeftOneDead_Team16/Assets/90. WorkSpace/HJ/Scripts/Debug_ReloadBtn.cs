using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_ReloadBtn : MonoBehaviour
{
    public GunController gt;

    public void ClickButton2Reload()
    {
        Debug.Log("재장전");
        gt.GunAction.Reload();
    }
}
