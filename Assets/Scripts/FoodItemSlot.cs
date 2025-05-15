using FoodSystem;
using JetBrains.Annotations;
using UnityEngine;

public class FoodItemSlot: MonoBehaviour
{
   [CanBeNull] public FoodItem foodItem;

   public string FoodItemName => foodItem != null ? foodItem.FoodItemName : "No Food Item";
   public void SetFoodItem(FoodItem item)
   {
      foodItem = item;
   }
}