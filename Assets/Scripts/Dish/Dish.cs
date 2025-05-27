using FoodSystem;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private FoodItem foodItem;

    public FoodItem FoodItem => foodItem;
}
