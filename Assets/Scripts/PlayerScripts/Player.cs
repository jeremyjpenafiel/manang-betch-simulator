using FoodSystem;
using Order;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PlayerScripts
{
    public class Player: MonoBehaviour
    {
        [ReadOnly, SerializeField] private Tray tray;
        [ReadOnly, SerializeField] private BeanInteraction beanInteraction;
    
        private void OnEnable()
        {
            tray = GetComponent<Tray>();
            beanInteraction = GetComponent<BeanInteraction>();

            beanInteraction.OnFoodAddToTray += AddFoodToTray;
            beanInteraction.OnFoodAddToTray += (FoodItem item) =>
            {
                OrderChecker.Instance.CheckOrder(tray.Dish, tray.Rice);
            };
        }

        private void AddFoodToTray(FoodItem item)
        {
            if (item.FoodItemName == "Rice")
            {
                tray.SetRice(item);
            }
            else
            {
                tray.SetDish(item);
            }
        }
    }
}