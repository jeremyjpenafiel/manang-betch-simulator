using UnityEngine;

namespace FoodSystem
{
    public class RecipeTextGroup: MonoBehaviour
    {
        [SerializeField] public IngredientText recipeName;
        [SerializeField] public IngredientButton cookButton;


        public void Initialize()
        {
            recipeName.Initialize();
        }
        
    }
}