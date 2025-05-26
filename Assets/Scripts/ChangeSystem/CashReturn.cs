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
        private void OnMouseOver()
        {
            Debug.Log("asdasd");
            if (Input.GetMouseButtonDown(0))
            {
                OnMoneyRemoved?.Invoke(gameObject);
            }
        }

        public void RegisterRemoveMoneyListener(Action listener)
        {
            OnMoneyRemoved += listener;
        }
    }
}
