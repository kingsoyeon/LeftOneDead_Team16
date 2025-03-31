using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelectUI : PopupUI
{
    public void OnChapterButtonSelected()
    {
        UIManager.Instance.ShowPopup<StageSelectUI>("StageSelectUI");
    }
}
