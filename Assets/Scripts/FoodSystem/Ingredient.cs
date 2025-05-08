using System;
using UnityEngine;

namespace FoodSystem
{
   public class Ingredient
   {
      public readonly IngredientData IngredientData;
      private int _quantity;
      public string Name => IngredientData.Name;

      public int Quantity
      {
         get => _quantity;
         set
         {
            if (value < 0)
            {
               _quantity = 0;
            }
            else
            {
               _quantity = value;
            }
         
            OnQuantityChanged?.Invoke(_quantity);
         }
      }
   
      public event Action<int> OnQuantityChanged; 
   
      public Ingredient(IngredientData ingredientData)
      {
         IngredientData = ingredientData;
      }
   
   }
}
