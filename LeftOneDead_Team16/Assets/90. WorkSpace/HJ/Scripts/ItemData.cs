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
/// ������ ���� �Ӽ�
/// </summary>
public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;

    public ItemType itemType;
}
