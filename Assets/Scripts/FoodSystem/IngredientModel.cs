using System.Collections.Generic;
using UnityEngine;

namespace FoodSystem
{
  public class IngredientModel 
  {
    public readonly List<Ingredient> Ingredients  = new();
  
    public void AddIngredient(Ingredient ingredient)
    {
      Ingredients.Add(ingredient);
    }
  }
}
