using TMPro;
using UnityEngine;

namespace FoodSystem
{
    public class RequiredIngredientsTextGenerator: MonoBehaviour
    {
        public Transform instantiationParent;
        public TextMeshProUGUI prefab;
        public string[] textLines; // Array of strings for dynamic text
         
        public void Awake() {
            for (int i = 0; i < textLines.Length; i++) {
                // Instantiate the prefab at the specified position
                var newText = Instantiate(prefab, instantiationParent);
                 
                // Set the text on the new object
                newText.text = textLines[i];
                 
                // Optionally, position the new object (e.g., in a grid)
                newText.transform.position = new Vector3(0, i*10, 0); // Adjust as needed
            }
        }
    }
}