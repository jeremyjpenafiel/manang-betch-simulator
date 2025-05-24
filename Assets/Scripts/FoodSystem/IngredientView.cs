using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace FoodSystem
{
    public class IngredientView : MonoBehaviour
    {
        [SerializeField] TMP_FontAsset font;
        [SerializeField] int ingredientTextYOffset = -65;
        [SerializeField] int ingredientTextXCoordinate = 200;
        [SerializeField] int ingredientQuantityXCoordinate = 0;
        
        [FormerlySerializedAs("ingredients")]
        [Header("Ingredients")]
        [SerializeField] public List<IngredientTextGroup> ingredientsTextGroups;
        [Header("Food Items")]
        [SerializeField] public List<RecipeTextGroup> recipeTextGroups;
        
        


        public void Initialize()
        {
            for(int i =0; i < ingredientsTextGroups.Count; i++)
            {
                ingredientsTextGroups[i].addIngredientButton.Initialize(i);
                ingredientsTextGroups[i].Initialize();
            }
            
            for(int i =0; i < recipeTextGroups.Count; i++)
            {
                recipeTextGroups[i].cookButton.Initialize(i);
                recipeTextGroups[i].Initialize();
            }
        }


        public void AddRequiredIngredientsTexts(int textGroupIndex, List<IngredientRequirement> requirements)
        {
            Transform recipeNameTransform = recipeTextGroups[textGroupIndex].recipeName.transform;
            
            

            for (int i = 0; i < requirements.Count; i++)
            {
                IngredientRequirement ingredientRequirement = requirements[i];
                
                // ingredient names
                TextMeshProUGUI ingredientName = new GameObject().AddComponent<TextMeshProUGUI>();
                ingredientName.transform.SetParent(recipeNameTransform);
                
                            
                
                ingredientName.text = ingredientRequirement.Ingredient.IngredientName;
                ingredientName.font = font;
                ingredientName.fontSize = 50;
                ingredientName.fontStyle = FontStyles.Bold;
                ingredientName.enableWordWrapping = false;
                ingredientName.UpdateFontAsset();
                ingredientName.transform.localPosition = new Vector3(ingredientTextXCoordinate, (i+1)*ingredientTextYOffset, 0);
                
                //ingredient quantities
                TextMeshProUGUI ingredientQuantity = new GameObject().AddComponent<TextMeshProUGUI>();
                ingredientQuantity.transform.SetParent(recipeNameTransform);
                
                ingredientQuantity.text = $"{ingredientRequirement.Ingredient.Quantity}/{ingredientRequirement.Amount}";
                ingredientQuantity.font = font;
                ingredientQuantity.fontSize = 50;
                ingredientQuantity.fontStyle = FontStyles.Bold;
                ingredientQuantity.enableWordWrapping = false;
                ingredientQuantity.UpdateFontAsset();
                ingredientQuantity.transform.localPosition = new Vector3(ingredientQuantityXCoordinate, (i+1)*ingredientTextYOffset, 0);
                
                ingredientRequirement.Ingredient.OnQuantityChanged += (quantity) =>
                {
                    ingredientQuantity.text = $"{quantity}/{ingredientRequirement.Amount}";
                };
            }
            
            
        }
    }
}
