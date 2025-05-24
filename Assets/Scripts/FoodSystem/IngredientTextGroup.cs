using UnityEngine;

namespace FoodSystem
{
    public class IngredientTextGroup: MonoBehaviour
    {
        [SerializeField] public IngredientText ingredientName;
        [SerializeField] public IngredientText ingredientQuantity;
        [SerializeField] public IngredientText ingredientPrice;
        [SerializeField] public IngredientButton addIngredientButton;

        public void Initialize()
        {
            ingredientName.Initialize();
            ingredientQuantity.Initialize();
            ingredientPrice.Initialize();
        }
        
        public void UpdateTexts(Ingredient ingredient)
        {
            ingredientName.UpdateText(ingredient.IngredientName);
            ingredientQuantity.UpdateText(ingredient.Quantity.ToString());
            ingredientPrice.UpdateText(ingredient.Price.ToString("F2"));
        }
        public void InitializeButton(bool interactable)
        {
            addIngredientButton.ChangeInteractable(interactable);
        }
    }
}