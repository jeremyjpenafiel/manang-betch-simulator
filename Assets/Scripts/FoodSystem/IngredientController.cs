using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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

        }

        private void ConnectIngredientModel()
        {
            for (int i = 0; i < _ingredientModel.Ingredients.Count; i++)
            {
                Ingredient ingredient = _ingredientModel.Ingredients[i];
                IngredientText ingredientText = _ingredientView.ingredientNames[i];
                ingredient.OnQuantityChanged += (quantity) =>
                {
                    ingredientText.UpdateIngredientQuantity($"{ingredient.IngredientName}: {quantity}");
                };
            }
        }

        private void ConnectIngredientView()
        {
            foreach (IngredientButton button in _ingredientView.addIngredientButtons)
            {
                button.RegisterListener(OnAddButtonPressed);
            }

        }

        private void OnAddButtonPressed(int index)
        {
            if (_ingredientModel.Ingredients[index] != null)
            {
                _ingredientModel.Ingredients[index].Quantity++;
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
      
        }
    }
}
