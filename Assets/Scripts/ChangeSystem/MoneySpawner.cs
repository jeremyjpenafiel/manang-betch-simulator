using System.Collections.Generic;
using UnityEngine;
using PosSystem;

namespace ChangeSystem
{
    public class MoneySpawner : MonoBehaviour
    {
        [SerializeField] private ChangeView changeView;
        [SerializeField] private PosView posView; // Prefab to instantiate
        [SerializeField] private PlayerStatistics playerStatistics; // Prefab to instantiate
        private Transform _moneySpawnPoint;
        private readonly Stack<GameObject> _moneyStack = new();
        private float currentChangeValue = 0f;
        private float calculatedChange = 0f;
        public PosController posController;
        

        public void OnMoneyAdded(GameObject money)
        {
            InstantiateMoney(money);
        }

        private void InstantiateMoney(GameObject money)
        {
            // Instantiate the money prefab at the spawn point
            if (money != null)
            {
                CashRegisterMoney cashRegisterMoney = money.GetComponent<CashRegisterMoney>();
                //Destroy(cashRegisterMoney);
                Debug.Log("Money prefab found, instantiating...");
                Debug.Log(money.name);
                var obj = Instantiate(money, transform.position, Quaternion.Euler(0, 0, 0));
                Rigidbody rb = obj.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                rb.useGravity = true;
                rb.mass = 500f;
                rb.AddForce(Vector3.down * 7000f, ForceMode.Acceleration);
                _moneyStack.Push(obj);

                currentChangeValue += cashRegisterMoney.value;
                Debug.Log($"Money value: {cashRegisterMoney.value}");
                changeView.UpdateChangeAmount(currentChangeValue);

            }
            else
            {
                Debug.LogError("Money prefab not found in Resources folder.");
            }
        }

        public void OnMoneyRemoved()
        {
            Debug.Log("DEPOTA");
            while (_moneyStack.Count > 0)
            {
                GameObject top = _moneyStack.Pop();
                Destroy(top);
            }

            currentChangeValue = 0f;
            changeView.UpdateChangeAmount(currentChangeValue);
        }

        public void OnMoneyReleased()
        {
            calculatedChange = posView.GetCalculatedChange();
            if (calculatedChange > currentChangeValue)
            {
                Debug.LogWarning("Not enough money to release change.");
                return;
            }
            Debug.Log("Releasing money...");
            playerStatistics.Money -= currentChangeValue;
            OnMoneyRemoved();
            posView.CloseChangeSystemScreen();
            posView.ClearPriceText();
            posView.UpdateTotalPriceText(0f);
            posView.CloseCashRegister();
            //update pos view
            posView.UpdateCalculatedChangeText("");
            posView.UpdateCashPaidText("");
            //PosController.cashPaid = 0f;
            //close change system
        }
    }
}