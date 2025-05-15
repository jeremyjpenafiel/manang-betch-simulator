using System.Collections.Generic;

namespace PosSystem
{
    public class PosModel
    {
        public List<FoodItemSlot> FoodItemSlots = new();

        public void AddFoodItemSlot(FoodItemSlot foodItemSlot)
        {
            FoodItemSlots.Add(foodItemSlot);
        }
    }
}