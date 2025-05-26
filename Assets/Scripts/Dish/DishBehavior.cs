using UnityEngine;

public class DishBehavior : MonoBehaviour
{
    private bool isHeld = false;

    public void SetHeld(bool held)
    {
        isHeld = held;
    }

    private void OnMouseDown()
    {
        if (isHeld) return;

        GameObject bean = GameObject.Find("Bean(Clone)");
        if (bean == null) return;

        Transform hand = bean.transform.Find("Hand");
        if (hand == null) return;

        // Clear previous slot
        DishSpawner spawner = FindObjectOfType<DishSpawner>();
        if (spawner != null)
            spawner.ClearDishFromSpawn(gameObject);

        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        isHeld = true;

        Collider col = GetComponent<Collider>();
        if (col) col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Debug.Log($"{gameObject.name} picked up and attached to hand.");
    }
}
