using System;
using System.Collections.Generic;
using UnityEngine;

namespace PosSystem
{
    public class PosController
    {
        private readonly PosModel _posModel;
        private readonly PosView _posView;
        private PlayerStatistics _playerStatistics;
        private float totalPrice;
        
        public PosController(PosModel posModel, PosView posView, PlayerStatistics playerStatistics)
        {
            _posModel = posModel;
            _posView = posView;
            Debug.Log("constructor");
            _playerStatistics = playerStatistics;

            ConnectModel();
            ConnectView();
        }

        private void ConnectModel()
        {
           for (int i = 0; i < _posModel.FoodItemSlots.Count; i++)
           {
               Debug.Log("for loop");
               FoodItemSlot foodItemSlot = _posModel.FoodItemSlots[i];
               try
               {
                   PosButton mealButton = _posView.mealButtons[i];
                   foodItemSlot.OnFoodItemChanged += () =>
                   { 
                       mealButton.RegisterListener(() => { 
                           if (foodItemSlot.foodItem == null) return; 
                           _posView.UpdatePriceText(foodItemSlot.FoodItemName + foodItemSlot.foodItem.UserPrice); }); 
                   };

                  
               }
               catch (ArgumentOutOfRangeException e)
               {
                   Debug.LogError($"PosController - ConnectView(): Button" +
                                  $"objects may not match number of food item slots");
                   Debug.LogError(e);
               }

           }
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
                        _posView.UpdatePriceText(foodItemSlot.FoodItemName + "  -   " + foodItemSlot.foodItem.UserPrice);
                        totalPrice += foodItemSlot.foodItem.UserPrice;
                        _posView.UpdateTotalPriceText(totalPrice);
                    });

                }
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.LogError($"PosController - ConnectView(): Button" +
                                    $"objects may not match number of food item slots");
                    Debug.LogError(e);
                }

            }

            for (int i = 0; i < _posView.transactionButtons.Count; i++)
            {
                int index = i; // Capture loop variable
                _posView.transactionButtons[i].RegisterListener(() =>
                {
                    if (index == 0)
                    {
                        _posView.OpenChangeSystemSreen();
                        _posView.OpenCashRegister();
                        _playerStatistics.Money += totalPrice;
                        
                    }
                    else if (index == 1)
                    {
                        // Call a different method for transactionButton(1)
                        _posView.ShowGcashPaymentReceipt();
                        _playerStatistics.Money += totalPrice;
                        _posView.ClearPriceText();
                        totalPrice = 0;
                        _posView.UpdateTotalPriceText(totalPrice);
                    }
                });
            }

            _posView.resetButton.RegisterListener(() =>
            {
                _posView.ClearPriceText();
                totalPrice = 0;
                _posView.UpdateTotalPriceText(totalPrice);
            });

        }
        
        
        public class Builder
        {
            private readonly PosModel _posModel = new();

            public PosController Build(PosView posView, PlayerStatistics playerStatistics)
            {
                return new PosController(_posModel, posView, playerStatistics);
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

