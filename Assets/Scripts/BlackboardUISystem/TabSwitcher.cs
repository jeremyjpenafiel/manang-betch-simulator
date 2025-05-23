using UnityEngine;
using UnityEngine.UIElements;

namespace BlackboardUISystem
{
    public class TabSwitcher: MonoBehaviour
    {
        [SerializeField] private GameObject ingredientTab;
        [SerializeField] private GameObject recipeTab;
        
        
        [SerializeField] private GameObject proteinTab;
        [SerializeField] private GameObject vegetableTab;
        [SerializeField] private GameObject condimentTab;
        
        
        public void SwitchToCondimentTab()
        {
            condimentTab.SetActive(true);
            proteinTab.SetActive(false);
            vegetableTab.SetActive(false);
        }
        
        public void SwitchToProteinTab()
        {
            proteinTab.SetActive(true);
            vegetableTab.SetActive(false);
            condimentTab.SetActive(false);
        }
        
        public void SwitchToVegetableTab()
        {
            proteinTab.SetActive(false);
            vegetableTab.SetActive(true);
            condimentTab.SetActive(false);
        }
        
        public void SwitchToIngredientTab()
        {
            SwitchToProteinTab();
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