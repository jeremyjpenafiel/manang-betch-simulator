using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PosSystem
{
    public class PosView: MonoBehaviour
    {
        [SerializeField] public List<PosButton> mealButtons;
        [SerializeField] private List<PosButton> transactionButtons;
        [SerializeField] private PosButton paymentButtons;
        [SerializeField] private TextMeshProUGUI priceText;


        private void Awake()
        {
            for (int i = 0; i < mealButtons.Count; i++)
            {
                mealButtons[i].Initialize(i);
            }

            priceText.text = "";
        }
        
        public void UpdatePriceText(string text)
        {
            priceText.text += $"{text}\n";
        }
    }
}