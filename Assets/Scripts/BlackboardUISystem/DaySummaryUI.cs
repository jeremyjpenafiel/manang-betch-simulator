using System;
using UnityEngine;
using TMPro;

public class DaySummaryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStatistics playerStatistics;

    [SerializeField] private TMP_Text totalIncomeText;
    [SerializeField] private TMP_Text successfulTransactionsText;
    [SerializeField] private TMP_Text missedOrdersText;
    [SerializeField] private TMP_Text foodThrownText;

    private void Start()
    {
        DisplaySummary();
    }

    public void DisplaySummary()
    {
        totalIncomeText.text = $"Total Income: ₱{playerStatistics.IncomeThusFar:F2}";
        successfulTransactionsText.text = $"Successful Transactions: {playerStatistics.Money / 50:F0}"; // Example logic
        missedOrdersText.text = $"Missed Orders: {playerStatistics.MissedOrders}";
        foodThrownText.text = $"Food Thrown: {playerStatistics.ThrownFoodTimes}";
    }
}
