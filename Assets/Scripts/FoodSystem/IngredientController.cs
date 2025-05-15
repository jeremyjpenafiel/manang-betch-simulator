using System;
using System.Collections.Generic;
using UnityEngine;

namespace FoodSystem
{
    public class IngredientController
    {
        private readonly IngredientModel _ingredientModel;
        private readonly IngredientView _ingredientView;
        private readonly FoodItemModel _foodItemModel;
        
        IngredientController(IngredientModel ingredientModel, IngredientView ingredientView, FoodItemModel foodItemModel)
        {
            _ingredientModel = ingredientModel;
            _ingredientView = ingredientView;
            _foodItemModel = foodItemModel;

            ConnectIngredientModel();
            ConnectIngredientView();
            ConnectFoodItemModel();

        }

        private void ConnectFoodItemModel()
        {
            for (int i = 0; i < _foodItemModel.FoodItems.Count; i++)
            {
                FoodItem foodItem = _foodItemModel.FoodItems[i];
                try
                {
                    IngredientText ingredientText = _ingredientView.foodItemNames[i];
                    foodItem.OnQuantityChanged += (quantity) =>
                    {
                        ingredientText.UpdateText($"{foodItem.FoodItemName}: {quantity}");
                    };
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.LogError($"IngredientController - ConnectFoodItemModel(): TextMeshProUGUI " +
                                   $"objects may not match number of food items");
                }
          
            }
            
            for (int j = 0; j < _foodItemModel.FoodItems.Count; j++)
            {
                FoodItem foodItem = _foodItemModel.FoodItems[j];
                try
                {
                    IngredientButton button = _ingredientView.addFoodItemButton[j];
                    foodItem.Initialize();
                    foodItem.OnPurchasableChanged += button.ChangeInteractable;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.LogError($"IngredientController - ConnectFoodItemModel(): Button " +
                                   $"objects may not match number of food items");
                }
                    
            }
        }

        private void ConnectIngredientModel()
        {
            for (int i = 0; i < _ingredientModel.Ingredients.Count; i++)
            {
                Ingredient ingredient = _ingredientModel.Ingredients[i];
                try
                {
                    IngredientText ingredientText = _ingredientView.ingredientNames[i];
                    ingredient.OnQuantityChanged += (quantity) =>
                    {
                        ingredientText.UpdateText($"{ingredient.IngredientName}: {quantity}");
                    };
                }
                catch (ArgumentOutOfRangeException e)
                {
                    
                    Debug.LogError($"IngredientController - ConnectIngredientModel(): TextMeshProUGUI " +
                                   $"objects may not match number of ingredients");
                }
          
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
            
            public Builder WithFoodItems(List<FoodItem> foodItems)
            {
                foreach (FoodItem foodItem in foodItems)
                {
                    _foodItemModel.AddFoodItem(foodItem);
                }
                return this;
            }
      
        }
    }
}
