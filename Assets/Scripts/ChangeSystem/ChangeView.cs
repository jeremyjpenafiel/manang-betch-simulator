using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ChangeSystem
{
    public class ChangeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI changeAmountText;
        [SerializeField] private List<CashRegisterMoney> cashRegisterBills;
        [SerializeField] private List<CashRegisterMoney> cashRegisterCoins;
        [SerializeField] private MoneySpawner billSpawner;
        [SerializeField] private MoneySpawner coinSpawner;
        [SerializeField] private CashReturn cashReturn;
        [SerializeField] private CashRelease cashRelease;



        private void Awake()
        {
            foreach (CashRegisterMoney cashRegisterBill in cashRegisterBills)
            {
                // if (changeAmountText == null)
                // {
                //     Debug.LogError("Change amount text is not assigned in ChangeView.");
                //     return;

                // }
                cashRegisterBill.RegisterAddMoneyListener(billSpawner.OnMoneyAdded);
                // cashRegisterBill.RegisterAddMoneyListener((GameObject _) =>
                // {
                //     Debug.Log($"Added money: {cashRegisterBill.value}");
                //     changeAmountText.text = $"{cashRegisterBill.value:F2}";
                // });

            }
            foreach (CashRegisterMoney cashRegisterCoin in cashRegisterCoins)
            {
                cashRegisterCoin.RegisterAddMoneyListener(coinSpawner.OnMoneyAdded);
            }

            cashReturn.RegisterRemoveMoneyListener(() =>
            {
                Debug.Log("Cash return clicked, removing all money.");
                billSpawner.OnMoneyRemoved();
                coinSpawner.OnMoneyRemoved();
            });

            cashRelease.RegisterReleaseMoneyListener(() =>
            {
                Debug.Log("Cash release clicked, releasing all money (change).");
                billSpawner.OnMoneyReleased();
                coinSpawner.OnMoneyReleased();

            });

        }

        public void UpdateChangeAmount(float text)
        {
            changeAmountText.text = text.ToString("F2");
        }
    }
}