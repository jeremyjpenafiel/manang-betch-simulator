using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FoodSystem
{
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient/Create new ingredient")]
    public class Ingredient: ScriptableObject
    {
        [FormerlySerializedAs("name")] [SerializeField] private string ingredientName;
        [SerializeField] private int quantity;
        [SerializeField] private float price;
        // [SerializeField] private Units unit;
        
        public event Action<int> OnQuantityChanged;
        public event Action<bool> OnPurchasableChanged;

        public Ingredient(string ingredientName, int quantity, float price)
        {
            this.ingredientName = ingredientName;
            this.quantity = quantity;
            this.price = price;
        }
    
        public string IngredientName => ingredientName;
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnQuantityChanged?.Invoke(quantity);
            }
        }

        public float Price => price;
    }
    
    [Serializable]
    public enum Units
    {
        Kilograms,
        Liters,
        Pieces
    }
}
