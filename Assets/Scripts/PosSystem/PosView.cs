using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PosSystem
{
    public class PosView : MonoBehaviour
    {
        [SerializeField] public List<PosButton> mealButtons;
        [SerializeField] public List<PosButton> transactionButtons;
        [SerializeField] public List<PosButton> changeButtons;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI totalPriceText;
        [SerializeField] private TextMeshProUGUI currentChangeSystemText;
        [SerializeField] private TextMeshProUGUI changeSystemText;
        [SerializeField] private GameObject cashRegisterOpen;
        [SerializeField] private GameObject gcashPaymentReceipt;
        [SerializeField] private GameObject changeSystemScreen;
        [SerializeField] public PosButton resetButton;




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

            for (int i = 0; i < changeButtons.Count; i++)
            {
                changeButtons[i].Initialize(i);
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
        
        public void UpdateCurrentChangeSystemText(string text)
        {
            currentChangeSystemText.text = text;
            Debug.Log($"Current Change System Text Updated: {text}");
        }
    }
}
