using UnityEngine;
using System.Collections.Generic;
using PlayerScripts;

public class GlassFoodDisplay : MonoBehaviour, IDishInteractable
{
    [Header("Food Display Slots")]
    [SerializeField] private List<Transform> foodDisplaySlots;

    public List<Transform> FoodDisplaySlots => foodDisplaySlots;

    public Transform GetFirstEmptySlot()
    {
        foreach (Transform slot in foodDisplaySlots)
        {
            if (slot.childCount == 0)
            {
                return slot;
            }
        }

        Debug.Log("No empty display slot available.");
        return null;
    }

    public void Interact(BeanInteraction beanInteraction, GameObject dish)
    {
        beanInteraction.state = PlayerStates.HandsFree;
        Transform t = GetFirstEmptySlot();
        dish.transform.SetParent(t);
        dish.transform.position = t.position;
        dish.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        var foodItemSlot = t.GetComponent<FoodItemSlot>();
        var dishReference = dish.GetComponent<DishReference>();

        foodItemSlot.FoodItem = dishReference.foodItem;

    }
}
