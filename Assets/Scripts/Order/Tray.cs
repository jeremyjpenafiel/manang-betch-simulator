using System;
using FoodSystem;
using UnityEngine;

namespace Order
{
    public class Tray: MonoBehaviour
    {
        public FoodItem Dish;
        public FoodItem Rice;
        
        public void SetDish(FoodItem foodItem)
        {
            Dish = foodItem;
        }

        public void SetRice(FoodItem item)
        {
            Rice = item;
        }

        public void ResetOrder()
        {
            Rice = null;
            Dish = null;
        }

        public Order PlayerAssembledOrder => new Order(Dish, Rice);
    }
}