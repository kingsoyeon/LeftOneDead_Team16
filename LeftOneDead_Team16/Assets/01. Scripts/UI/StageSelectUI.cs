using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectUI : PopupUI
{
   public void OnStageSelectButton()
    {
        GameManager.Instance.StageStart();
    }
}
