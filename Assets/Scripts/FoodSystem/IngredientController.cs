using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace FoodSystem
{
    public class IngredientController
    {
        private readonly IngredientModel _ingredientModel;
        private readonly IngredientView _ingredientView;
        private readonly FoodItemModel _foodItemModel;
        // private readonly FoodItemView _foodItemView;
        
        IngredientController(IngredientModel ingredientModel, IngredientView ingredientView, FoodItemModel foodItemModel)
        {
            _ingredientModel = ingredientModel;
            _ingredientView = ingredientView;
            _foodItemModel = foodItemModel;
            // _foodItemView = foodItemView;

            ConnectIngredientModel();
            ConnectIngredientView();
            ConnectFoodItemModel();

        }

        private void ConnectFoodItemModel()
        {
            for (int i = 0; i < _foodItemModel.FoodItems.Count; i++)
            {
                FoodItemData foodItem = _foodItemModel.FoodItems[i];
                IngredientText ingredientText = _ingredientView.foodItemNames[i];
                foodItem.OnQuantityChanged += (quantity) =>
                {
                    ingredientText.UpdateText($"{foodItem.FoodItemName}: {quantity}");
                };
            }
        }

        private void ConnectIngredientModel()
        {
            for (int i = 0; i < _ingredientModel.Ingredients.Count; i++)
            {
                Ingredient ingredient = _ingredientModel.Ingredients[i];
                IngredientText ingredientText = _ingredientView.ingredientNames[i];
                ingredient.OnQuantityChanged += (quantity) =>
                {
                    ingredientText.UpdateText($"{ingredient.IngredientName}: {quantity}");
                };
                // ingredient.OnQuantityChanged 
            }

            for (int j = 0; j < _foodItemModel.FoodItems.Count; j++)
            {
                IngredientButton button = _ingredientView.addFoodItemButton[j];
                FoodItemData foodItem = _foodItemModel.FoodItems[j];
                foodItem.Initialize();
                foodItem.OnPurchasableChanged += button.ChangeInteractable;
            }
        }

        private void ConnectIngredientView()
        {
            foreach (IngredientButton button in _ingredientView.addIngredientButtons)
            {
                button.RegisterListener(OnIngredientBought);
            }

            foreach (IngredientButton button in _ingredientView.addFoodItemButton)
            {
                button.RegisterListener(OnFoodItemBought);
            }

        }
        
        // buttonIndex is the index of the food item in the list
        private void OnFoodItemBought(int buttonIndex)
        {
            _foodItemModel.FoodItems[buttonIndex].Quantity++;
            _foodItemModel.FoodItems[buttonIndex].Purchase();
            
        }

        // buttonIndex is the index of the ingredient in the list
        private void OnIngredientBought(int buttonIndex)
        {
            if (_ingredientModel.Ingredients[buttonIndex] != null)
            {
                _ingredientModel.Ingredients[buttonIndex].Quantity++;
            }
        }

        public class Builder
        {
            private readonly IngredientModel _ingredientModel = new();
            private readonly FoodItemModel _foodItemModel = new();
            public IngredientController Build(IngredientView ingredientView)
            {
                return new IngredientController(_ingredientModel, ingredientView, _foodItemModel);
            }
            
            public Builder WithIngredients(List<Ingredient> ingredients)
            {
                foreach (Ingredient ingredient in ingredients)
                {
                    _ingredientModel.AddIngredient(ingredient);
                }
                return this;
            }
            
            public Builder WithFoodItems(List<FoodItemData> foodItems)
            {
                foreach (FoodItemData foodItem in foodItems)
                {
                    _foodItemModel.AddFoodItem(foodItem);
                }
                return this;
            }
      
        }
    }
}
