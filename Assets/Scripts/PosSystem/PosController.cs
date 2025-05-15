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


            ConnectModel();
            ConnectView();
        }

        private void ConnectModel()
        {
           
        }

        private void ConnectView()
        {
             for(int i = 0; i< _posModel.FoodItemSlots.Count; i++)
             {
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

