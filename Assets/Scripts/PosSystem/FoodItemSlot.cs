using System;
using FoodSystem;
using JetBrains.Annotations;
using UnityEngine;

public class FoodItemSlot : MonoBehaviour
{
   [CanBeNull, SerializeField] private FoodItem foodItem;
   public string FoodItemName => foodItem != null ? foodItem.FoodItemName : "No Food Item";


   public event Action OnFoodItemChanged;
   public FoodItem FoodItem
   {
      get => foodItem;
      set
      {
         foodItem = value;
         OnFoodItemChanged?.Invoke();
      }
   }
   

}



