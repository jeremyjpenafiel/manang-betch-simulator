using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ChangeSystem
{
    public class ChangeView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI changeAmountText;
        [SerializeField] private List<CashRegisterMoney> cashRegisterBills;
        [SerializeField] private List<CashRegisterMoney> cashRegisterCoins;
        [SerializeField] private MoneySpawner billSpawner;
        [SerializeField] private MoneySpawner coinSpawner;

        
        private void Awake()
        {
            foreach (CashRegisterMoney cashRegisterBill in cashRegisterBills)
            {
                cashRegisterBill.RegisterAddMoneyListener(billSpawner.OnMoneyAdded);
                cashRegisterBill.RegisterRemoveMoneyListener(billSpawner.OnMoneyRemoved);
                
            }
            foreach (CashRegisterMoney cashRegisterCoin in cashRegisterCoins)
            {
                cashRegisterCoin.RegisterAddMoneyListener(coinSpawner.OnMoneyAdded);
                cashRegisterCoin.RegisterRemoveMoneyListener(coinSpawner.OnMoneyRemoved);
            }
        }
    }
}