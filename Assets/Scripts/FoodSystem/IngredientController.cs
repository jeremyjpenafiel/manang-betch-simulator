using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            foreach (Ingredient ingredient in _ingredientModel.Ingredients)
            {
                ingredient.OnQuantityChanged += _ingredientView.UpdateIngredientQuantity;
            }
        }

        private void ConnectIngredientView()
        {
           _ingredientView._addIngredientButton.RegisterListener(OnAddButtonPressed);
        }

        private void OnAddButtonPressed(int index)
        {
            Debug.Log($"Add button pressed for index: {index}");
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
            
            public Builder WithIngredients(List<IngredientData> ingredientData)
            {
                foreach (IngredientData data in ingredientData)
                {
                    Debug.Log($"Adding ingredient data: {data.Name}");
                    Ingredient ingredient = new Ingredient(data);
                    Debug.Log($"Adding ingredient: {ingredient.IngredientData.Name}");
                    _ingredientModel.AddIngredient(ingredient);
                }

                return this;
            }
      
        }
    }
}
