using System.Collections.Generic;
using UnityEngine;

namespace PosSystem
{
    public class PosSystem: MonoBehaviour
    {
        [SerializeField] private PosView posView;
        [SerializeField] private List<FoodItemSlot> foodItemSlots;

        PosController _posController;

        private void Awake()
        {
            _posController = new PosController.Builder()
                .WithFoodItemSlots(foodItemSlots)
                .Build(posView);
        }
    }
}