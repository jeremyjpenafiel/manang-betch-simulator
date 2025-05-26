using System;
using System.Collections.Generic;
using UnityEngine;

namespace PosSystem
{
    public class PosController
    {
        private readonly PosModel _posModel;
        private readonly PosView _posView;
        
        public PosController(PosModel posModel, PosView posView)
        {
            _posModel = posModel;
            _posView = posView;
            Debug.Log("constructor");

            ConnectModel();
            ConnectView();
        }

        private void ConnectModel()
        {
           
        }

        private void ConnectView()
        {
            Debug.Log("Connect view before loop");

            for (int i = 0; i < _posModel.FoodItemSlots.Count; i++)
            {
                Debug.Log("for loop");
                FoodItemSlot foodItemSlot = _posModel.FoodItemSlots[i];
                try
                {
                    PosButton mealButton = _posView.mealButtons[i];
                    mealButton.RegisterListener(() =>
                    {
                        if (foodItemSlot.foodItem == null) return;
                        _posView.UpdatePriceText(foodItemSlot.FoodItemName + foodItemSlot.foodItem.UserPrice);
                    });
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.LogError($"PosController - ConnectView(): Button" +
                                    $"objects may not match number of food item slots");
                }

            }

            for (int i = 0; i < _posView.transactionButtons.Count; i++)
            {
                int index = i; // Capture loop variable
                _posView.transactionButtons[i].RegisterListener(() =>
                {
                    if (index == 0)
                    {
                        _posView.OpenCashRegister();
                    }
                    else if (index == 1)
                    {
                        // Call a different method for transactionButton(1)
                        _posView.ShowGcashPaymentReceipt();
                    }
                });
            }

        }
        
        
        public class Builder
        {
            private readonly PosModel _posModel = new();

            public PosController Build(PosView posView)
            {
                return new PosController(_posModel, posView);
            }
            
            public Builder WithFoodItemSlots(List<FoodItemSlot> foodItemSlots)
            {
                foreach (FoodItemSlot foodItemSlot in foodItemSlots)
                {
                    _posModel.AddFoodItemSlot(foodItemSlot);
                }
                return this;
            }
        }
    }
    
    
}

