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
        [SerializeField] private GameObject cashRegisterOpen;
        [SerializeField] private GameObject gcashPaymentReceipt;


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
        }

        public void UpdatePriceText(string text)
        {
            priceText.text += $"{text}\n";
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