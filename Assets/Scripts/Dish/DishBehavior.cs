using UnityEngine;

public class DishBehavior : MonoBehaviour
{
    private bool isHeld = false;

    private void OnMouseDown()
    {
        if (isHeld) return; // Prevent multiple pick-ups

        // Find the Bean GameObject (you can tag it "Player" if needed)
        GameObject bean = GameObject.FindWithTag("Player");
        if (bean == null)
        {
            Debug.LogWarning("Bean not found.");
            return;
        }

        // Find the hand transform
        Transform hand = bean.transform.Find("Hand");
        if (hand == null)
        {
            Debug.LogWarning("Hand transform not found in Bean.");
            return;
        }

        // Attach this dish to the hand
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero; // offset
        transform.localRotation = Quaternion.identity;
        isHeld = true;

        Collider col = GetComponent<Collider>();
        if (col) col.enabled = false;

        Debug.Log($"{gameObject.name} picked up and attached to hand.");
    }
}