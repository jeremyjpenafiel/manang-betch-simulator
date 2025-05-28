using System;
using System.Collections.Generic;
using Order;
using UnityEngine;
using FoodSystem;

namespace PosSystem
{
    public class PosController
    {
        private readonly PosModel _posModel;
        private readonly PosView _posView;
        private PlayerStatistics _playerStatistics;
        private PaymentGenerator _paymentGenerator;
        private float totalPrice;
        public float change;
        public float cashPaid;

        private List<FoodItem> foodItems;
        private bool isOrderCorrect;

        public event Action<bool> OnOrderChecked;
        
        public PosController(PosModel posModel, PosView posView, PlayerStatistics playerStatistics, PaymentGenerator paymentGenerator)
        {
            _posModel = posModel;
            _posView = posView;
            _playerStatistics = playerStatistics;
            _paymentGenerator = paymentGenerator;

            foodItems = new List<FoodItem>();

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
                        foodItems.Add(foodItemSlot.FoodItem);
                        CheckInputOrder();
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

                // Register interactibility based on correct order
                OnOrderChecked += _posView.transactionButtons[i].SetInteractable;

                _posView.transactionButtons[i].RegisterListener(() =>
                {
                    if (index == 0)
                    {
                        _posView.OpenChangeSystemSreen();
                        _posView.OpenCashRegister();
                        _playerStatistics.Money += totalPrice;
                        //add cash paid
                        cashPaid = _paymentGenerator.GetPayment(totalPrice);
                        _posView.UpdateCashPaidText(cashPaid.ToString("F2"));
                        //calculate change
                        change = cashPaid - totalPrice;
                        _posView.SetCalculatedChange(change);
                        _posView.UpdateCalculatedChangeText(change.ToString("F2"));



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
                foodItems.Clear();
            });

        }

        private void CheckInputOrder()
        {
            Debug.Log("Checking order...");
            if (foodItems.Count != 2)
            {
                isOrderCorrect = false;
                Debug.LogWarning("Incorrect order quantity.");
                OnOrderChecked?.Invoke(isOrderCorrect);
                return;
            }
            if (OrderChecker.Instance.CheckOrder(foodItems[0], foodItems[1]))
            {
                isOrderCorrect = true;
                OnOrderChecked?.Invoke(isOrderCorrect);
                Debug.Log("Order is correct.");
                return;
            }
            else if (OrderChecker.Instance.CheckOrder(foodItems[1], foodItems[0]))
            {
                isOrderCorrect = true;
                OnOrderChecked?.Invoke(isOrderCorrect);
                Debug.Log("Order is correct.");
                return;
            }
            else
            {
                isOrderCorrect = false;
                OnOrderChecked?.Invoke(isOrderCorrect);
                Debug.LogWarning("Order is incorrect.");
            }
        }

        
        
        public class Builder
        {
            private readonly PosModel _posModel = new();

            public PosController Build(PosView posView, PlayerStatistics playerStatistics, PaymentGenerator paymentGenerator)
            {
                return new PosController(_posModel, posView, playerStatistics, paymentGenerator);
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

