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
        UpdateMoneyText(playerStatistics.Money);
    }

    private void UpdateMoneyText(float money)
    {
        moneyText.text = "Php" + money.ToString("F2");
    }
}