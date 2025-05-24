using System;
using FoodSystem;
using UnityEngine;

namespace Testing
{
    public class TestUtils : MonoBehaviour
    {
        [SerializeField] private PlayerStatistics playerStatistics;

        public void ResetIngredientsQuantities()
        {
            var ingredients = Resources.LoadAll<Ingredient>("Ingredients");
            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity = 0;
                Debug.Log($"Resetting quantity of {ingredient.IngredientName} to {ingredient.Quantity}");
            }
        }
    
        public void SetIngredientQuantities(int quantity)
        {
            var ingredients = Resources.LoadAll<Ingredient>("Ingredients");
            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity = quantity;
            }
        }
    
        public void SetPlayerMoney(int money)
        {
            try
            {
                playerStatistics.Money = money;
            }
            catch (NullReferenceException e)
            {
                Debug.LogError($"Failed to set player money: {e.Message}");
            }
        }
    }
}
