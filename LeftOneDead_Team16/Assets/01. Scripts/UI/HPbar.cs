using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : ScreenUI
{
    [SerializeField] private Image barImage;
    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        float value = player.Data.Condition.currentHealth / player.Data.Condition.maxHealth;
        barImage.fillAmount = value;
    }
}
