using System;
using FoodSystem;

namespace Order
{
    [Serializable]  
    public struct Order
    {
        public FoodItem meal;
        public FoodItem rice;

        public Order (FoodItem meal, FoodItem rice)
        {
            this.meal = meal;
            this.rice = rice;
        }
    }
}