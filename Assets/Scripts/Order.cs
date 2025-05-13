using System.Collections.Generic;
using FoodSystem;

public class Order
{
   public List<FoodItem> FoodItems { get; }

   public float TotalPrice { get; private set; }

   public Order(List<FoodItem> foodItems)
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
