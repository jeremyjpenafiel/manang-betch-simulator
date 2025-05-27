using UnityEngine;
using System.Collections.Generic;
public class GlassFoodDisplay : MonoBehaviour
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
}
