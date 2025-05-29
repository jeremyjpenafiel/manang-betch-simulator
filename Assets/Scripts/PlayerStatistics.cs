using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerStatistics", menuName = "ScriptableObjects/PlayerStatistics")]
public class PlayerStatistics : ScriptableObject
{
    [SerializeField] private float money;
    [SerializeField] private float capital;

    [SerializeField, ReadOnly] private float dailyIncome;

    [SerializeField, ReadOnly] private int missedOrders;

    [SerializeField, ReadOnly] private int thrownFoodTimes;

    public int SuccessfulTransactions;
    

    public event Action OnMoneyChanged; 
    public float Money
    {
        get => money;
        set
        {
            money = value;
            OnMoneyChanged?.Invoke();
        }
    }

    public int ThrownFoodTimes
    {
        get => thrownFoodTimes;
        set => thrownFoodTimes = value;
    }

    public float DailyIncome => dailyIncome;

    public float IncomeThusFar => capital - money;
    public float MissedOrders => missedOrders;
}

