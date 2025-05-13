using System.Collections.Generic;
using UnityEngine;

namespace FoodSystem
{
    public class FoodItemModel : MonoBehaviour
    {
        public readonly List<FoodItemData> FoodItems = new();
    
    
        public void AddFoodItem(FoodItemData foodItemData)
        {
            FoodItems.Add(foodItemData);
        }
    }
}
