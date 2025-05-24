using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerStatistics", menuName = "ScriptableObjects/PlayerStatistics")]
public class PlayerStatistics : ScriptableObject
{
    [SerializeField] private float money;

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
}

