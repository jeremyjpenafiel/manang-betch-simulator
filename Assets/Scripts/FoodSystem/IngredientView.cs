using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FoodSystem
{
    public class IngredientView : MonoBehaviour
    {
        [SerializeField] public List<IngredientText> ingredientNames;
        [SerializeField] public List<IngredientButton> addIngredientButtons;


        private void Awake()
        {
            for(int i =0; i < ingredientNames.Count; i++)
            {
                addIngredientButtons[i].Initialize(i);
            }
        }
    }
}
