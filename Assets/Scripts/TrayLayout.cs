using UnityEngine;
using FoodDisplay;
using UnityEngine.Serialization;

public class TrayLayout : MonoBehaviour
{
    [FormerlySerializedAs("servingSlots")] [SerializeField] private Transform dishServingSlot;
    [FormerlySerializedAs("riceTransform")] [SerializeField] private Transform riceServingSlot;
    private bool isServingonTray = false;
    private bool isRiceOnTray = false;
    private int currentSlotIndex = 0;

    public bool TryPlaceOnTray(GameObject serving)
    {
        if (!serving.TryGetComponent(out Serving servingType)) return false;

        bool isServingADish = servingType.FoodType == FoodType.Dish;
        Transform slot = isServingADish ? dishServingSlot : riceServingSlot;
        
        if (!isServingonTray && isServingADish)
        {
            serving.transform.SetParent(slot);
            serving.transform.localPosition = Vector3.zero;
            serving.transform.localRotation = Quaternion.identity;
            isServingonTray = true;
            return true;
        }
        if (!isRiceOnTray && !isServingADish)
        {
            
            serving.transform.SetParent(slot);
            serving.transform.localPosition = Vector3.zero;
            serving.transform.localRotation = Quaternion.identity;
            isRiceOnTray = true;
            return true;
        }

        Debug.Log("No available slots on the tray.");
        return false;
    }

    public void ResetTray()
    {
        currentSlotIndex = 0;
    }
     public bool HasAvailableSlot()
    {
        return !isServingonTray;
    }
}
