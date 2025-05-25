using UnityEngine;

public class DishClickHandler : MonoBehaviour
{
    private Transform handTransform;

    public void SetPlayer(GameObject player)
    {
        // Find the "Hand" object in the player prefab (by name or tag or assigned manually)
        handTransform = player.transform.Find("Hand");

        if (handTransform == null)
        {
            Debug.LogError("Hand transform not found on player!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && handTransform != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.CompareTag("Dish"))
                {
                    clickedObject.transform.SetParent(handTransform);
                    clickedObject.transform.localPosition = Vector3.zero;
                    clickedObject.transform.localRotation = Quaternion.identity;

                    // Optional: disable physics so it doesn't fall
                    if (clickedObject.TryGetComponent<Rigidbody>(out var rb))
                    {
                        rb.isKinematic = true;
                    }
                }
            }
        }
    }
}
