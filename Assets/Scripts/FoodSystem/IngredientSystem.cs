using System;
using System.Collections;
using System.Collections.Generic;
using FoodSystem;
using UnityEngine;

public class IngredientSystem : MonoBehaviour
{
    [SerializeField] IngredientView ingredientView;
    [Header("Protein")]
    [SerializeField] List<Ingredient> proteinList;
    [Header("Fruits and Vegetables")]
    [SerializeField] List<Ingredient> fruitsAndVegetables;
    [Header("Condiments and Seasonings")]
    [SerializeField] List<Ingredient> condimentsAndSeasonings;
    
    [SerializeField] List<FoodItem> foodItems;
    [SerializeField] List<FoodItem> foodItemList;
    [SerializeField] PlayerStatistics playerStatistics;
    IngredientController ingredientController;

    private void Awake()
    {
        ingredientController = new IngredientController.Builder()
            .WithIngredients(proteinList)
            .WithIngredients(fruitsAndVegetables)
            .WithIngredients(condimentsAndSeasonings)
            .WithFoodItems(foodItems)
            .Build(ingredientView, playerStatistics);
    }
}
