using System;
using System.Collections;
using System.Collections.Generic;
using FoodSystem;
using UnityEngine;

public class IngredientSystem : MonoBehaviour
{
    [SerializeField] IngredientView ingredientView;
    [SerializeField] List<Ingredient> ingredientDataList;
    [SerializeField] List<FoodItem> foodItems;
    [SerializeField] List<FoodItem> foodItemList;
    IngredientController ingredientController;

    private void Awake()
    {
        ingredientController = new IngredientController.Builder()
            .WithIngredients(ingredientDataList)
            .WithFoodItems(foodItems)
            .Build(ingredientView);
    }
}
