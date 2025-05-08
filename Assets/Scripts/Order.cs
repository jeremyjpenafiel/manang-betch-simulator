using System.Collections.Generic;
using FoodSystem;

public class Order
{
   public List<FoodItemData> FoodItems { get; }

   public float TotalPrice { get; private set; }

   public Order(List<FoodItemData> foodItems)
   {
      FoodItems = foodItems;
      CalculateTotalPrice();
   }
   
   private void CalculateTotalPrice()
   {
      TotalPrice = 0;
      foreach (var foodItem in FoodItems)
      {
         TotalPrice += foodItem.CostPrice;
      }
   }
}
