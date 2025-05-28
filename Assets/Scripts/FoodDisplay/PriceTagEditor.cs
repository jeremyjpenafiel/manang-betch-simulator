using UnityEngine;
using TMPro;
using FoodSystem;

public class FoodPriceEditor : MonoBehaviour
{
    [Header("References")]
    public TMP_Text priceLabel;                     // UI display label
    public TMP_InputField priceInputField;          // UI input field
    public FoodItemSlot foodItemSlot;               // Reference to the food slot

    private void Start()
    {
        // Initialize UI
        RefreshPriceUI();
        priceInputField.gameObject.SetActive(false);

        // Listen to end edit
        priceInputField.onEndEdit.AddListener(ApplyNewPrice);
        foodItemSlot.OnFoodItemChanged += RefreshPriceUI;
    }

    public void OnPriceTagClicked()
    {
        if (foodItemSlot.FoodItem == null) return;

        priceInputField.gameObject.SetActive(true);
        priceInputField.text = foodItemSlot.FoodItem.UserPrice.ToString("F2");
        priceInputField.Select();
        priceInputField.ActivateInputField();
    }

    private void ApplyNewPrice(string input)
    {
        if (foodItemSlot.FoodItem == null) return;

        if (float.TryParse(input, out float newPrice))
        {
            foodItemSlot.FoodItem.UserPrice = newPrice;
            RefreshPriceUI();
        }

        priceInputField.gameObject.SetActive(false);
    }

    private void RefreshPriceUI()
    {
        if (foodItemSlot.FoodItem != null)
        {
            priceLabel.text = $"₱{foodItemSlot.FoodItem.UserPrice:F2}";
        }
        else
        {
            priceLabel.text = "₱0.00";
        }
    }
}
