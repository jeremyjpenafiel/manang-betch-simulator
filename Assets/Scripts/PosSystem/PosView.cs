using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PosSystem
{
    public class PosView : MonoBehaviour
    {
        [Header("POS")]
        [SerializeField] public List<PosButton> mealButtons;
        [SerializeField] public List<PosButton> transactionButtons;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI totalPriceText;
        [SerializeField] private GameObject gcashPaymentReceipt;

        [Header("Change System")]
        [SerializeField] private TextMeshProUGUI cashPaidText;
        [SerializeField] private TextMeshProUGUI calculatedChangeText;
        [SerializeField] private GameObject cashRegisterOpen;
        [SerializeField] private GameObject changeSystemScreen;
        [SerializeField] public PosButton resetButton;

        [Header("Order")]
        private GameObject trayInPaymentSection;


        //private float change;
        private float calculatedChange;




        private void Awake()
        {
            priceText.text = "";
            resetButton.Initialize(0);

            for (int i = 0; i < mealButtons.Count; i++)
            {
                mealButtons[i].Initialize(i);
            }

            for (int i = 0; i < transactionButtons.Count; i++)
            {
                transactionButtons[i].Initialize(i);
                transactionButtons[i].SetInteractable(false); // Initially set transaction buttons to not interactable
            }
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
            cashRegisterOpen.SetActive(true);
            Debug.Log("Cash Register Opened");
        }

        public void CloseCashRegister()
        {
            cashRegisterOpen.SetActive(false);
            Debug.Log("Cash Register Closed");
        }

        public void ShowGcashPaymentReceipt()
        {
            // Implement logic to show Gcash payment receipt
            gcashPaymentReceipt.SetActive(true);
            Debug.Log("Gcash Payment Receipt Shown");

            DestroyTrayInPaymentSection();
            Invoke(nameof(HideGcashPaymentReceipt), 1f);
        }

        private void HideGcashPaymentReceipt()
        {
            gcashPaymentReceipt.SetActive(false);
            Debug.Log("Gcash Payment Receipt Hidden");
        }

        public void OpenChangeSystemSreen()
        {
            changeSystemScreen.SetActive(true);
            Debug.Log("Change System Screen Opened");
        }

        public void CloseChangeSystemScreen()
        {
            changeSystemScreen.SetActive(false);
            Debug.Log("Change System Screen Closed");
        }

        // public void UpdateCurrentChangeSystemText(string text)
        // {
        //     currentChangeSystemText.text = text;
        //     Debug.Log($"Current Change System Text Updated: {text}");
        // }

        public void UpdateCashPaidText(string text)
        {
            cashPaidText.text = text;
            Debug.Log($"Cash Paid Text Updated: {text}");
        }

        public void UpdateCalculatedChangeText(string text)
        {
            calculatedChangeText.text = text;
            Debug.Log($"Calculated Change Text Updated: {text}");
        }

        public void SetCalculatedChange(float change)
        {
            calculatedChange = change;
        }
        public float GetCalculatedChange()
        {
            return calculatedChange;
        }

        public void SetTrayInPaymentSection(GameObject tray)
        {
            trayInPaymentSection = tray;
            Debug.Log("Tray set in payment section.");
        }

        public void DestroyTrayInPaymentSection()
        {
            if (trayInPaymentSection != null)
            {
                Destroy(trayInPaymentSection);
                Debug.Log("Tray in payment section destroyed.");
                trayInPaymentSection = null;
            }
        }

    }
}
