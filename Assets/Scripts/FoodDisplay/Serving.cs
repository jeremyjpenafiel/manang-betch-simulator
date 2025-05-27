using UnityEngine;

namespace FoodDisplay
{
    public class Serving:MonoBehaviour
    {
        [SerializeField] public FoodType FoodType;
    }

    public enum FoodType
    {
        Dish,
        Rice
    }
}