using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PosSystem
{
    public class PosView : MonoBehaviour
    {
        [SerializeField] public List<PosButton> mealButtons;
        [SerializeField] public List<PosButton> transactionButtons;
        [SerializeField] public PosButton paymentButtons;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI totalPriceText;
        [SerializeField] private GameObject cashRegisterOpen;
        [SerializeField] private GameObject gcashPaymentReceipt;
        [SerializeField] public PosButton resetButton;



        private void Awake()
        {
            for (int i = 0; i < mealButtons.Count; i++)
            {
                mealButtons[i].Initialize(i);
            }

            priceText.text = "";

            //paymentButtons.Initialize(0);
            for (int i = 0; i < transactionButtons.Count; i++)
            {
                transactionButtons[i].Initialize(i);
            }

            resetButton.Initialize(0);
        }


        public void UpdatePriceText(string text)
        {
            priceText.text += $"{text}\n";
        }

        public void ClearPriceText()
        {
            priceText.text = "";
            totalPriceText.text = "";
        }

        public void UpdateTotalPriceText(float text)
        {

            Debug.Log($"UpdateTotalPriceText: {text}");
            totalPriceText.text = text.ToString("F2");
        }

        public void OpenCashRegister()
        {
            if (!cashRegisterOpen.activeSelf)
            {
                cashRegisterOpen.SetActive(true);
                Debug.Log("Cash Register Opened");
            }
            else
            {
                cashRegisterOpen.SetActive(false);
                Debug.Log("Cash Register Closed");
            }
        }

        public void ShowGcashPaymentReceipt()
        {
            // Implement logic to show Gcash payment receipt
            gcashPaymentReceipt.SetActive(true);
            Debug.Log("Gcash Payment Receipt Shown");
            Invoke(nameof(HideGcashPaymentReceipt), 1f);
        }

        private void HideGcashPaymentReceipt()
        {
            gcashPaymentReceipt.SetActive(false);
            Debug.Log("Gcash Payment Receipt Hidden");
        }
    }
}
