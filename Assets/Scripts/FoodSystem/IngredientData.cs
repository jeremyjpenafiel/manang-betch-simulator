using System;
using UnityEngine;

namespace FoodSystem
{
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient/Create new ingredient")]
    public class IngredientData: ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private int quantity;
        [SerializeField] private float price;
        [SerializeField] private Units unit;

        public IngredientData(string name, int quantity, float price)
        {
            this.name = name;
            this.quantity = quantity;
            this.price = price;
        }
    
        public string Name { get; }
        public int Quantity { get; }

        public float Price
        {
            get => price;
            private set => price = value;
        }
    }
    
    [Serializable]
    public enum Units
    {
        Kilograms,
        Liters,
        Pieces
    }
}
