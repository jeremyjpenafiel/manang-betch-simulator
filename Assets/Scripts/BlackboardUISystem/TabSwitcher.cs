using UnityEngine;
using UnityEngine.UIElements;

namespace BlackboardUISystem
{
    public class TabSwitcher: MonoBehaviour
    {
        
        [SerializeField] private GameObject ingredientTab;
        [SerializeField] private GameObject recipeTab;
        
        [Header("Ingredient Category Pages")]
        [SerializeField] private GameObject proteinTab;
        [SerializeField] private GameObject vegetableTab;
        [SerializeField] private GameObject condimentTab;

        [Header("Recipe Pages")] 
        [SerializeField] private GameObject recipeFirstPage;
        [SerializeField] private GameObject recipeSecondPage;
        [SerializeField] private GameObject recipeThirdPage;

        public void OpenBlackboardd()
        {
            SwitchToIngredientTab();
            SwitchToRecipeFirstPage();
        }
        
        
        public void SwitchToRecipeFirstPage()
        {
            recipeFirstPage.SetActive(true);
            recipeSecondPage.SetActive(false);
            recipeThirdPage.SetActive(false);
        }
        
        public void SwitchToRecipeSecondPage()
        {
            recipeFirstPage.SetActive(false);
            recipeSecondPage.SetActive(true);
            recipeThirdPage.SetActive(false);
        }
        public void SwitchToRecipeThirdPage()
        {
            recipeFirstPage.SetActive(false);
            recipeSecondPage.SetActive(false);
            recipeThirdPage.SetActive(true);
        }
        
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