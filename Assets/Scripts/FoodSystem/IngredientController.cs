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
        private readonly PlayerStatistics _playerStatistics;
        
        
        IngredientController(IngredientModel ingredientModel, 
            IngredientView ingredientView, 
            FoodItemModel foodItemModel,
            PlayerStatistics playerStatistics)
        
        {
            _ingredientModel = ingredientModel;
            _ingredientView = ingredientView;
            _foodItemModel = foodItemModel;
            _playerStatistics = playerStatistics;
            
            ingredientView.Initialize();

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
                    IngredientText ingredientText = _ingredientView.recipeTextGroups[i].recipeName;
                    ingredientText.Initialize();
                    ingredientText.UpdateText(foodItem.FoodItemName);
                    foodItem.OnQuantityChanged += (quantity) =>
                    {
                        ingredientText.UpdateText(quantity.ToString());
                    };
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.LogError($"IngredientController - ConnectFoodItemModel(): TextMeshProUGUI " +
                                   $"objects may not match number of food items");
                }
                
                try
                {
                    IngredientButton button = _ingredientView.recipeTextGroups[i].cookButton;
                    foodItem.OnPurchasableChanged += button.ChangeInteractable;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.LogError($"IngredientController - ConnectFoodItemModel(): Button " +
                                   $"objects may not match number of food items");
                }

                try
                {
                    _ingredientView.AddRequiredIngredientsTexts(i, foodItem.RequiredIngredients);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
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
                    IngredientTextGroup ingredientTextGroup = _ingredientView.ingredientsTextGroups[i];
                    ingredientTextGroup.Initialize();
                    ingredientTextGroup.InitializeButton(_playerStatistics.Money >= ingredient.Price);
                    ingredientTextGroup.UpdateTexts(ingredient);
                    ingredient.OnQuantityChanged += (quantity) =>
                    {
                        ingredientTextGroup.ingredientQuantity.UpdateText(quantity.ToString());
                    };
                    _playerStatistics.OnMoneyChanged += () =>
                    {
                        ingredientTextGroup.addIngredientButton.ChangeInteractable(
                            _playerStatistics.Money >= ingredient.Price);
                    };
                }
                catch (ArgumentOutOfRangeException e)
                {
                    
                    Debug.LogError($"IngredientController - ConnectIngredientModel(): IngredientTextGroup " +
                                   $"objects may not match number of ingredients");
                }
          
            }
        }

        private void ConnectIngredientView()
        {
            foreach (IngredientTextGroup ingredientTextGroup in _ingredientView.ingredientsTextGroups)
            {
                ingredientTextGroup.addIngredientButton.RegisterListener(OnIngredientBought);
            }

            foreach (RecipeTextGroup recipeTextGroup in _ingredientView.recipeTextGroups)
            {
                recipeTextGroup.cookButton.RegisterListener(OnFoodItemBought);
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
                _playerStatistics.Money -= _ingredientModel.Ingredients[buttonIndex].Price;
                
            }
        }

        public class Builder
        {
            private readonly IngredientModel _ingredientModel = new();
            private readonly FoodItemModel _foodItemModel = new();
            public IngredientController Build(IngredientView ingredientView, PlayerStatistics playerStatistics)
 
            {
                return new IngredientController(_ingredientModel, ingredientView, _foodItemModel, playerStatistics);
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
