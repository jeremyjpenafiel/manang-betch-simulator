using System;
using UnityEngine;

namespace FoodSystem
{
    [Serializable]
    public struct IngredientRequirement
    {
        [SerializeField] private Ingredient ingredient;

        [SerializeField] private int amount;

        public Ingredient Ingredient => ingredient;
        public int Amount => amount;
    }
}
