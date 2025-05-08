using System;
using UnityEngine;

namespace FoodSystem
{
   public class FoodItem : MonoBehaviour
   {
      public readonly FoodItemData FoodItemData;
      private int _quantity;
   
      public event Action<int> OnQuantityChanged;
   
      public int Quantity
      {
         get => _quantity;
         set
         {
            if (value < 0)
            {
               Debug.LogError("Quantity cannot be negative");
               return;
            }
            _quantity = value;
            OnQuantityChanged?.Invoke(_quantity);
         }
      }
   
   
      public FoodItem(FoodItemData foodItemData)
      {
         FoodItemData = foodItemData;
      }
   }
}
