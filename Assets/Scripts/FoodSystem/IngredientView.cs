using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FoodSystem
{
    public class IngredientView : MonoBehaviour
    {
        [Header("Ingredients")]
        // [SerializeField] public List<IngredientText> ingredientNames;
        // [SerializeField] public List<IngredientText> ingredientQuantities;
        // [SerializeField] public List<IngredientText> ingredientPrices;
        [SerializeField] public List<IngredientTextGroup> ingredients;
        // [SerializeField] public List<IngredientButton> addIngredientButtons;
        [Header("Food Items")]
        [SerializeField] public List<IngredientText> foodItemNames;
        [SerializeField] public List<IngredientButton> addFoodItemButton;


        private void Awake()
        {
            for(int i =0; i < ingredients.Count; i++)
            {
                ingredients[i].addIngredientButton.Initialize(i);
            }
            
            for(int i =0; i < foodItemNames.Count; i++)
            {
                addFoodItemButton[i].Initialize(i);
            }
        }
    }
}
