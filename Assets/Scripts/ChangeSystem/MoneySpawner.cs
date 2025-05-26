using System.Collections.Generic;
using UnityEngine;

namespace ChangeSystem
{
    public class MoneySpawner: MonoBehaviour
    {
        private Transform _moneySpawnPoint;
        private readonly Stack<GameObject> _moneyStack = new();
        private float currentChangeValue = 0f;
        
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
                var obj = Instantiate(money, transform.position, Quaternion.Euler(0,0,0));
                Rigidbody rb  = obj.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                _moneyStack.Push(obj);
                
            }
            else
            {
                Debug.LogError("Money prefab not found in Resources folder.");
            }
        }
        
        public void OnMoneyRemoved()
        {
            if (_moneyStack.TryPop(out GameObject top))
            {
                CashRegisterMoney cashRegisterMoney = top.GetComponent<CashRegisterMoney>();
                currentChangeValue -= top.value;
                Destroy(top);
            
            }
            else
            {
                Debug.Log("No money to remove.");
            }
        }
    }
}