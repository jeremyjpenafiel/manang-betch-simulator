using System;
using FoodSystem;
using UnityEngine;

namespace Order
{
    public class OrderChecker: MonoBehaviour
    {
        public static OrderChecker Instance;
        [SerializeField] private OrderSystem _orderSystem;

        public void SetOrderSystem(OrderSystem system)
        {
            _orderSystem = system;
        }
        
        public  bool CheckOrder(FoodItem meal, FoodItem rice)
        {
            if (_orderSystem.CurrentOrder == null)
            {
                Debug.LogWarning("orderSystem order is null");
                return false;
            }
            var order = (Order)_orderSystem.CurrentOrder;
            if (order.meal == meal && order.rice == rice)
            {
                Debug.Log("Order is correct.");
                return true;
            }
                
            Debug.Log("Order is incorrect.");
            return false;

        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}