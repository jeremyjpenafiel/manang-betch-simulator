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
        Debug.Log(ingredientDataList.Count);
        Debug.Log(ingredientDataList[0].IngredientName);
        Debug.Log(ingredientDataList[0]);
        
        ingredientController = new IngredientController.Builder()
            .WithIngredients(ingredientDataList)
            .WithFoodItems(foodItems)
            .Build(ingredientView);
    }
}
