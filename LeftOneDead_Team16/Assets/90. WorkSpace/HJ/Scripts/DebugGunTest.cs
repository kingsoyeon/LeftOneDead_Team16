using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DebugGunTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GunController gt;
    private GunController.GunActions GunActions;
    private List<Button> Buttons = new List<Button>();

    public Button fireBtn;
    public Button reloadBtn;

    private void Start()
    {
        GunActions = gt.GunAction;

    }

    



    void ReloadBtnClicked()
    {
        Debug.Log("재장전버튼버튼다운");
        GunActions.Reload();
    }

    void FireBtnDown()
    {
        Debug.Log("발사버튼다운");
        GunActions.TriggerPull();
    }
    void FireBtnUp()
    {
        Debug.Log("발사버튼업");
        GunActions.TriggerRelease();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("포인터다운");
        if (eventData.pointerCurrentRaycast.gameObject == reloadBtn.gameObject)
        {
            ReloadBtnClicked();
        }
        if (eventData.pointerCurrentRaycast.gameObject == fireBtn.gameObject)
        {
            FireBtnDown();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == fireBtn.gameObject)
        {
            FireBtnUp();
        }
    }
}
