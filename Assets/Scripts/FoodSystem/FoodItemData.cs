using System.Collections.Generic;
using UnityEngine;

namespace FoodSystem
{
   [CreateAssetMenu(fileName = "FoodItem", menuName = "FoodItem/Create new food item")]
   public class FoodItemData: ScriptableObject

   {
      [SerializeField] private List<IngredientRequirement> requiredIngredients;

      public float CostPrice
      {
         get
         {
            float totalCost = 0;
            foreach (IngredientRequirement ingredientRequirement in requiredIngredients)
            {
               totalCost += ingredientRequirement.IngredientData.IngredientData.Price;
            }

            return totalCost;
         }
      }
   }
}
