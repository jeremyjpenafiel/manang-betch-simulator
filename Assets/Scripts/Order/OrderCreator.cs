using System;
using System.Collections.Generic;
using FoodSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Order
{
    public class OrderCreator: MonoBehaviour
    {
        [SerializeField] private List<FoodItemSlot> foodItemSlots;
        [SerializeField] private List<FoodItem> possibleMeals;
        [SerializeField] private FoodItem _rice;

        public static OrderCreator instance; 

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdatePossibleMeals()
        {
            Debug.Log("UPDATEE");
            possibleMeals.Clear();
            foreach (var slot in foodItemSlots)
            {
                if (slot.FoodItem == null) continue;
                possibleMeals.Add(slot.FoodItem);
            }
        }

        public Order CreateOrder()
        {
            // FoodItem randomFoodItem = possibleMeals[Random.Range(0, possibleMeals.Count)];
            return new Order(_rice, _rice);
        }
        
        public void SetRice(FoodItem riceItem)
        {
            _rice = riceItem;
        }
     
    }
}