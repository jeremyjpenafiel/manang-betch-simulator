using UnityEngine;
using System.Collections.Generic;

public class TrayLayout : MonoBehaviour
{
    [SerializeField] private Transform servingSlots;
    [SerializeField] private Transform riceTransform;
    private bool isServingonTray = false;
    private bool isRiceOnTray = false;
    private int currentSlotIndex = 0;

    public bool TryPlaceOnTray(GameObject serving)
    {
        if (!isServingonTray)
        {
            Transform slot = servingSlots;
            serving.transform.SetParent(slot);
            serving.transform.localPosition = Vector3.zero;
            serving.transform.localRotation = Quaternion.identity;
            isServingonTray = true;
            return true;
        }

        Debug.Log("No available slots on the tray.");
        return false;
    }

    public bool TryPlaceRiceOnTray(GameObject serving)
    {
        if (!isRiceOnTray)
        {
            Transform slot = riceTransform;
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
