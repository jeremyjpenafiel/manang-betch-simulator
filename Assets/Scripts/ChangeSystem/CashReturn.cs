using System;
using UnityEngine;

namespace ChangeSystem 
{
    public class CashReturn : MonoBehaviour
    {
        [SerializeField] private int value;
        [SerializeField] private GameObject moneyPrefab;
        public event Action<GameObject> OnMoneyAdded;
        public event Action OnMoneyRemoved;
        public void ResetCash()
        {
            OnMoneyRemoved?.Invoke();
        }

        // public void ReleaseMoney()
        // { 

        // }

        private void Awake()
        {


        }

        public void RegisterRemoveMoneyListener(Action listener)
        {
            OnMoneyRemoved += listener;
        }
    }
}
