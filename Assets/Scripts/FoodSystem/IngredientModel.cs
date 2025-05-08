using System.Collections.Generic;
using UnityEngine;

namespace FoodSystem
{
  public class IngredientModel 
  {
    public readonly List<Ingredient> Ingredients  = new();
  
    public void AddIngredient(Ingredient ingredient)
    {
      Debug.Log($"Adding ingredient in model: {ingredient.IngredientData.Name}");
      Ingredients.Add(ingredient);
    }
  }
}
