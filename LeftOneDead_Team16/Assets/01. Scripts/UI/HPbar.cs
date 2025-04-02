using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : ScreenUI
{
    [SerializeField] private Image barImage;
    [SerializeField] private TextMeshProUGUI hpText;
    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        float value = player.Data.Condition.currentHealth / player.Data.Condition.maxHealth;
         
        hpText.text = $"{player.Data.Condition.currentHealth}";

        if(player.Data.Condition.currentHealth < 1) { hpText.text = "0";  } 
        barImage.fillAmount = value;
    }
}
