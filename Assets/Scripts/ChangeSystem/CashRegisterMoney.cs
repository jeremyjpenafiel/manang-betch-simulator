using System;
using UnityEngine;

namespace ChangeSystem 
{
    public class CashRegisterMoney : MonoBehaviour
    {
        [SerializeField] public float value;
        [SerializeField] private GameObject moneyPrefab;
        public event Action<GameObject> OnMoneyAdded;
        public event Action<GameObject> OnMoneyRemoved;
        private void OnMouseOver()
        {
            Debug.Log("asdasd");
            if (Input.GetMouseButtonDown(0))
            {
                OnMoneyAdded?.Invoke(gameObject);
            }
            // else if (Input.GetMouseButtonDown(1))
            // {
            //     Debug.Log("Right click detected");
            //     OnMoneyRemoved?.Invoke(gameObject);
            // }
        }

        public void RegisterAddMoneyListener(Action<GameObject> listener)
        {
            OnMoneyAdded += listener;
        }
        // public void RegisterRemoveMoneyListener(Action<GameObject> listener)
        // {
        //     OnMoneyRemoved += listener;
        // }

        public GameObject GetMoneyPrefab()
        {
            return moneyPrefab;
        }
    }
}
