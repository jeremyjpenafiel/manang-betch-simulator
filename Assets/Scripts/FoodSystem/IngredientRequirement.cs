using System;
using UnityEngine;

namespace FoodSystem
{
    [Serializable]
    public struct IngredientRequirement
    {
        [SerializeField] private Ingredient _ingredient;

        [SerializeField] private int amount;

        public Ingredient IngredientData => _ingredient;
    }
}
