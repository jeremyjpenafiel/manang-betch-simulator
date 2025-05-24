using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FoodSystem 
{
    public class IngredientText : MonoBehaviour
    {
        private TextMeshProUGUI _ingredientName;

        public void Initialize()
        {
            _ingredientName = GetComponent<TextMeshProUGUI>();
        }
        public void UpdateText(string text)
        {
            _ingredientName.text = text;
        }
    }
}
