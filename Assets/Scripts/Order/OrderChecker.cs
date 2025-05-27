using System;
using FoodSystem;
using UnityEngine;

namespace Order
{
    public class OrderChecker: MonoBehaviour
    {
        private static OrderChecker _instance;
        public static Order Order;
        
        public static bool CheckOrder(FoodItem meal, FoodItem rice)
        {
            if (Order.meal == meal && Order.rice == rice)
            {
                Debug.Log("Order is correct.");
                return true;
            }

            Debug.Log("Order is incorrect.");
            return false;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}