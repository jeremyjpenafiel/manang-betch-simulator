using System.Collections.Generic;
using FoodSystem;
using Sirenix.OdinInspector;

namespace FoodDisplay
{
    public class FoodDisplayManager: SerializedMonoBehaviour
    {
        public Dictionary<FoodItemSlot, FoodItem> foodDisplaySlots = new Dictionary<FoodItemSlot, FoodItem>();
    }
}