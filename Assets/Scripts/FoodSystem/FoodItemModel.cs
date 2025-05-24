using System.Collections.Generic;
using UnityEngine;

namespace FoodSystem
{
    public class FoodItemModel
    {
        public readonly List<FoodItem> FoodItems = new();
    
    
        public void AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
        }

        public void Initialize()
        {
            foreach (FoodItem foodItem in FoodItems)
            {
                foodItem.Initialize();
            }
        }
    }
}
