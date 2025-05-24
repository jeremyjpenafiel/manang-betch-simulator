using System;
using TMPro;
using UnityEngine;

public class MoneyText: MonoBehaviour
{
    [SerializeField] private PlayerStatistics playerStatistics;
    [SerializeField] private TextMeshProUGUI moneyText;


    private void OnEnable()
    {
        playerStatistics.OnMoneyChanged += UpdateMoneyText;
        
    }

    public void Initialize()
    {
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "Php" + playerStatistics.Money.ToString("F2");
    }
}