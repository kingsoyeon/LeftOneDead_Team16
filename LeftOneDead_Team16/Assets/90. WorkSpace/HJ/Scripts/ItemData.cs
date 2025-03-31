using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Firearm,
    Ammo,
    Consumable
}


/// <summary>
/// 아이템 공통 속성
/// </summary>
public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;

    public ItemType itemType;
}
