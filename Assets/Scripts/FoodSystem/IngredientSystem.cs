using System;
using System.Collections;
using System.Collections.Generic;
using FoodSystem;
using UnityEngine;

public class IngredientSystem : MonoBehaviour
{
    [SerializeField] IngredientView ingredientView;
    [SerializeField] List<IngredientData> ingredientDataList;
    IngredientController ingredientController;

    private void Awake()
    {
        Debug.Log(ingredientDataList.Count);
        Debug.Log(ingredientDataList[0].Name);
        Debug.Log(ingredientDataList[0]);
        
        ingredientController = new IngredientController.Builder()
            .WithIngredients(ingredientDataList)
            .Build(ingredientView);
    }
}
