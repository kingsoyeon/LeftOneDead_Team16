using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumables", menuName = "GameData/Consumables")]
public class ItemDataConsumables : InteractablesSOBase
{
    public List<ConsumableTypeValue> Consumables = new List<ConsumableTypeValue>();
}

[System.Serializable]
public class ConsumableTypeValue
{
    public ConsumeType Type;
    public float Value;
    public float Duration;
}

public enum ConsumeType
{
    Health,
    HealthBonus,
    Stamina
}
