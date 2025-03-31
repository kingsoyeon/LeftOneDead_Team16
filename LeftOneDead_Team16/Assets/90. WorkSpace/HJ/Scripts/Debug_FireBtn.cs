using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Debug_FireBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GunController gt;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("방아쇠 당기기");
        gt.GunAction.TriggerPull();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("방아쇠 놓기");
        gt.GunAction.TriggerRelease();
    }
}
