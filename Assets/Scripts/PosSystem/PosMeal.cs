using FoodSystem;

namespace PosSystem
{
    public class PosMeal
    {
        public FoodItem FoodItem;

        public float Price => FoodItem.UserPrice;
        public string Name => FoodItem.FoodItemName;

    }
}