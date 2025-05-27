using UnityEngine;
using System.Collections.Generic;

public class TrayLayout : MonoBehaviour
{
    [SerializeField] private List<Transform> servingSlot;
    [SerializeField] private List<Transform> riceSlot;

    private bool servingUsed = false;
    private bool riceUsed = false;

    public bool TryPlaceOnTray(GameObject serving)
    {
        if (!servingUsed && servingSlot.Count > 0)
        {
            Transform slot = servingSlot[0];
            serving.transform.SetParent(slot);
            serving.transform.localPosition = Vector3.zero;
            serving.transform.localRotation = Quaternion.identity;
            servingUsed = true;
            return true;
        }

        Debug.Log("Serving slot is already used.");
        return false;
    }

    public bool TryPlaceRiceOnTray(GameObject rice)
    {
        if (!riceUsed && riceSlot.Count > 0)
        {
            Transform slot = riceSlot[0];
            rice.transform.SetParent(slot);
            rice.transform.localPosition = Vector3.zero;
            rice.transform.localRotation = Quaternion.identity;
            riceUsed = true;
            return true;
        }

        Debug.Log("Rice slot is already used.");
        return false;
    }

    public bool HasServingSlotAvailable()
    {
        return !servingUsed && servingSlot.Count > 0;
    }

    public bool HasRiceSlotAvailable()
    {
        return !riceUsed && riceSlot.Count > 0;
    }

    public void ResetTray()
    {
        servingUsed = false;
        riceUsed = false;
    }
}
