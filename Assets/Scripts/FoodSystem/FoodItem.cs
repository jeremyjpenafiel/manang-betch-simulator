using System;
using System.Collections.Generic;
using UnityEngine;

namespace FoodSystem
{
   [CreateAssetMenu(fileName = "FoodItem", menuName = "FoodItem/Create new food item")]
   public class FoodItem: ScriptableObject

   {
      [SerializeField] private string foodItemName;
      [SerializeField] private List<IngredientRequirement> requiredIngredients;
      [SerializeField] private int quantity;
      [SerializeField] private float userPrice;
      
      private bool _isPurchasable;
      public event Action<int> OnQuantityChanged;
      public event Action<bool> OnPurchasableChanged;

      public void Initialize()
      {
         foreach (IngredientRequirement ingredientRequirement in requiredIngredients)
         {
            Ingredient ingredient = ingredientRequirement.Ingredient;
            ingredient.OnQuantityChanged += CheckIfPurchasable;
            
         } 
         CheckIfPurchasable(0);
      }
      
      private bool IsPurchasable
      {
         get => _isPurchasable;
         set
         {
            _isPurchasable = value;
            OnPurchasableChanged?.Invoke(value);
         }
      }

      private void CheckIfPurchasable(int _)
      {
         foreach (IngredientRequirement ingredientRequirement in requiredIngredients)
         {
            if (ingredientRequirement.Ingredient.Quantity < ingredientRequirement.Amount)
            {
               IsPurchasable = false;
               return;
            }
         } 
         IsPurchasable = true;
         
      }
      public int Quantity 
      {
         get => quantity;
         set
         {
            quantity = value;
            OnQuantityChanged?.Invoke(quantity);
         }
      }

      public float UserPrice { get => userPrice; set => userPrice = value; }

      public float CostPrice
      {
         get
         {
            float totalCost = 0;
            foreach (IngredientRequirement ingredientRequirement in requiredIngredients)
            {
               totalCost += ingredientRequirement.Ingredient.Price;
            }

            return totalCost;
         }
      }

      public void Purchase()
      {
         SubtractIngredients();
         OnPurchasableChanged?.Invoke(IsPurchasable);
      }
      
      private void SubtractIngredients()
      {
         foreach (IngredientRequirement ingredientRequirement in requiredIngredients)
         {
            ingredientRequirement.Ingredient.Quantity -= ingredientRequirement.Amount;
         }
      }
      
      public string FoodItemName => foodItemName;
   }
}
