using System.Collections.Generic;
using FoodSystem;
using NPCSystem;
using UnityEngine;

namespace Order
{
    public class OrderSystem : MonoBehaviour
    {
        [SerializeField] List<FoodItem> possibleMeals;
        public Order? CurrentOrder { get; set; }

        public void Initialize()
        {
        }

        public void OnNewCustomer(Npc npc)
        {
            CurrentOrder = OrderCreator.instance.CreateOrder();
            Debug.Log($"New order created for NPC: {npc.name} - {CurrentOrder}");
            if (CurrentOrder == null)
            {
                Debug.LogError("CurrentOrder is null, cannot assign to NPC.");
                return;
            }
            npc.order = (Order)CurrentOrder;
            npc.orderText.text = $"Order: {npc.order.meal.FoodItemName} with Rice";
            Debug.Log($"Order assigned to NPC: {npc.name} - {npc.order}");
        }

        public void ResetOrder()
        {
            CurrentOrder = null;
        }
        

    }
}