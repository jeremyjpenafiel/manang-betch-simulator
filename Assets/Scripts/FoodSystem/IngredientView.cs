using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FoodSystem
{
    public class IngredientView : MonoBehaviour
    {
        public IngredientButton _addIngredientButton;
        public IngredientButton _removeIngredientButton;
        [SerializeField] private TextMeshProUGUI _ingredientQuantity;


        public void UpdateIngredientQuantity(int quantity)
        {
            _ingredientQuantity.text = quantity.ToString();
        }

        private void Awake()
        {
            _addIngredientButton.Initialize(0);
            // _removeIngredientButton.Initialize(1);
        }
    }
}
