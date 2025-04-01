using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour, IInteractable, IConsumable
{
    public ItemDataConsumables Data;

    public void Consume(PlayerSO playerSO)
    {
        foreach (var v in Data.Consumables)
        {
            switch (v.Type)
            {
                case ConsumeType.Health:
                    playerSO.Condition.currentHealth += v.Value;
                    playerSO.Condition.currentHealth = (playerSO.Condition.currentHealth > playerSO.Condition.maxHealth)? playerSO.Condition.maxHealth : playerSO.Condition.currentHealth;
                    break;
            }
        }
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
