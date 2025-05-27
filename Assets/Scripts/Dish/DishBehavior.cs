using System;
using UnityEngine;
using FoodSystem;
using JetBrains.Annotations;

public class DishBehavior : MonoBehaviour
{
    [SerializeField] private int dishQuantity = 10;
    [SerializeField] private GameObject servingPrefab;
    [SerializeField] [CanBeNull] private Ingredient _ingredient; //for rice only

    private void OnEnable()
    {
        if (_ingredient == null) return;
        _ingredient.OnQuantityChanged += UpdateDishQuantity;
    }

    private void UpdateDishQuantity(int ingredientQuantity)
    {
        dishQuantity = ingredientQuantity;
    }

    public bool TryServe(out GameObject serving)
    {
        if (dishQuantity > 0)
        {
            if (_ingredient)
            {
                _ingredient.Quantity--;
            }
            serving = Instantiate(servingPrefab);
            return true;
        }

        serving = null;
        return false;
    }

    public int GetRemainingQuantity()
    {
        return dishQuantity;
    }
}
