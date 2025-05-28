using System;
using System.Collections.Generic;
using Order;
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
            _playerStatistics = playerStatistics;

            ConnectModel();
            ConnectView();
        }

        private void ConnectModel()
        {
            OrderCreator.instance.SetFoodItemslots(_posModel.FoodItemSlots); 
            for (int i = 0; i < _posModel.FoodItemSlots.Count; i++)
            {
                FoodItemSlot foodItemSlot = _posModel.FoodItemSlots[i];
                try
                {
                    PosButton mealButton = _posView.mealButtons[i];
                    foodItemSlot.OnFoodItemChanged += () =>
                    { 
                        mealButton.RegisterListener(() => { 
                            if (foodItemSlot.FoodItem == null) return; 
                            //_posView.UpdatePriceText(foodItemSlot.FoodItemName + foodItemSlot.FoodItem.UserPrice); 
                            }); 
                    };

                    foodItemSlot.OnFoodItemChanged += () =>
                    {
                        OrderCreator.instance.UpdatePossibleMeals();
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

            for (int i = 0; i < _posModel.FoodItemSlots.Count; i++)
            {
                FoodItemSlot foodItemSlot = _posModel.FoodItemSlots[i];
                try
                {
                    PosButton mealButton = _posView.mealButtons[i];
                    mealButton.RegisterListener(() =>
                    {
                        if (foodItemSlot.FoodItem == null) return;
                        totalPrice += foodItemSlot.FoodItem.UserPrice;
                        _posView.UpdateTotalPriceText(totalPrice);
                        _posView.UpdatePriceText(foodItemSlot.FoodItemName + "  -   " + foodItemSlot.FoodItem.UserPrice);
                        
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

