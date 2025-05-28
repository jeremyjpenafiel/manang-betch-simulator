using System;
using UnityEngine;

namespace ChangeSystem 
{
    public class CashRelease : MonoBehaviour
    {
        [SerializeField] private int value;
        [SerializeField] private GameObject moneyPrefab;
        public event Action<GameObject> OnMoneyAdded;
        public event Action OnMoneyReleased;
        public void ReleaseCash()
        {
            OnMoneyReleased?.Invoke();
        }

        // public void ReleaseMoney()
        // { 

        // }

        private void Awake()
        {


        }

        public void RegisterReleaseMoneyListener(Action listener)
        {
            OnMoneyReleased += listener;
        }
    }
}
