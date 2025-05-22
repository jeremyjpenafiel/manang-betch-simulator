using UnityEngine;
using UnityEngine.UIElements;

namespace BlackboardUISystem
{
    public class TabSwitcher: MonoBehaviour
    {
        [SerializeField] private GameObject ingredientTab;
        [SerializeField] private GameObject recipeTab;
        
        
        public void SwitchToIngredientTab()
        {
            ingredientTab.SetActive(true);
            recipeTab.SetActive(false);
        }
        
        public void SwitchToRecipeTab()
        {
            ingredientTab.SetActive(false);
            recipeTab.SetActive(true);
        }
        
        
    }
}