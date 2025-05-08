using System.Collections.Generic;
using UnityEngine;

namespace FoodSystem
{
    public class FoodItemModel : MonoBehaviour
    {
        private readonly List<FoodItem> FoodItems = new List<FoodItem>();
    
    
        public void AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
        }
    }
}
