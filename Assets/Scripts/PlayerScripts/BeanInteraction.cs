using UnityEngine;

public class BeanInteraction : MonoBehaviour
{
    private GameObject heldDish = null;
    [SerializeField] private Transform hand;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clicked = hit.collider.gameObject;

                // PICK UP
                if (heldDish == null && clicked.CompareTag("Dish"))
                {
                    Transform originalParent = clicked.transform.parent; 
                    heldDish = clicked;

                    // Reparent to hand
                    heldDish.transform.SetParent(hand);
                    heldDish.transform.localPosition = Vector3.zero;
                    heldDish.transform.localRotation = Quaternion.identity;

                    // Disable collider
                    Collider col = heldDish.GetComponent<Collider>();
                    if (col) col.enabled = false;

                    // Clear spawn reference
                    DishSpawner spawner = originalParent.GetComponentInParent<DishSpawner>();
                    if (spawner != null)
                    {
                        spawner.ClearSpawnedDish(originalParent); 
                    }

                    Debug.Log($"{heldDish.name} picked up.");
                }

                // PLACE BACK ON TABLE
                else if (heldDish != null && clicked.CompareTag("Table"))
                {
                    DishSpawner spawner = clicked.GetComponent<DishSpawner>();
                    if (spawner == null)
                        spawner = clicked.GetComponentInParent<DishSpawner>();

                    if (spawner != null)
                    {
                        foreach (Transform spawnPoint in spawner.spawnPoints)
                        {
                            if (spawnPoint.childCount == 0)
                            {
                                heldDish.transform.SetParent(spawnPoint);
                                heldDish.transform.position = spawnPoint.position;
                                heldDish.transform.rotation = spawnPoint.rotation;

                                Collider col = heldDish.GetComponent<Collider>();
                                if (col) col.enabled = true;

                                // Update dish reference in spawner (optional)
                                spawner.SetDishAt(spawner.spawnPoints.IndexOf(spawnPoint), heldDish);

                                Debug.Log($"{heldDish.name} placed on table.");
                                heldDish = null;
                                return;
                            }
                        }

                        Debug.Log("No free spawn point on the table.");
                    }
                }
            }
        }
    }
}
