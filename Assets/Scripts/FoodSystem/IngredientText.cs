using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientText : MonoBehaviour
{
    private TextMeshProUGUI _ingredientName;

    private void Awake()
    {
        _ingredientName = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateIngredientQuantity(string text)
    {
        _ingredientName.text = text;
    }
}
