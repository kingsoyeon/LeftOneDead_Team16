using UnityEngine;

public class Generator : MonoBehaviour, IInteractable
{
    private int amountOfOil;
    [SerializeField] private int maxAmountOfOil;

    private void Awake()
    {
        amountOfOil = 0;
    }

    public void Interact()
    {
        if (amountOfOil == 0)
        {
            // 몬스터 웨이브 발생
        }

        AddOilToGenerator();
    }

    private void AddOilToGenerator()
    {
        if (amountOfOil == maxAmountOfOil)
        {
            return;
        }
        else if (amountOfOil < maxAmountOfOil)
        {
            amountOfOil++;
        }
    }
}