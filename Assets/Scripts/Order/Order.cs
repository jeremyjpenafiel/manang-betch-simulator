using System;
using FoodSystem;

namespace Order
{
    public class Order
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