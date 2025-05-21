using System.Collections.Generic;
using UnityEngine;

namespace ChangeSystem
{
    public class MoneySpawner: MonoBehaviour
    {
        private Transform _moneySpawnPoint;
        private readonly Stack<GameObject> _moneyStack = new();
        
        public void OnMoneyAdded(GameObject money)
        {
            InstantiateMoney(money);
        }

        private void InstantiateMoney(GameObject money)
        {
            // Instantiate the money prefab at the spawn point
            if (money != null)
            {
                Debug.Log("Money prefab found, instantiating...");
                Debug.Log(money.name);
                _moneyStack.Push(Instantiate(money, transform.position, Quaternion.identity));
                
            }
            else
            {
                Debug.LogError("Money prefab not found in Resources folder.");
            }
        }
        
        public void OnMoneyRemoved(GameObject money)
        {
            if (_moneyStack.TryPop(out GameObject top))
            {
                Destroy(top);
            }
            else
            {
                Debug.Log("No money to remove.");
            }
        }
    }
}