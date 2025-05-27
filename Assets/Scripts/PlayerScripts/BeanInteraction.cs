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
                    heldDish.transform.SetParent(hand);
                    heldDish.transform.localPosition = Vector3.zero;
                    heldDish.transform.localRotation = Quaternion.identity;

                    Collider col = heldDish.GetComponent<Collider>();
                    if (col) col.enabled = false;

                    // Clear from DishSpawner (spawn table)
                    DishSpawner spawner = originalParent.GetComponentInParent<DishSpawner>();
                    if (spawner != null)
                    {
                        spawner.ClearSpawnedDish(heldDish.transform);
                        Debug.Log($"{heldDish.name} picked up from spawn table.");
                    }
                    else
                    {
                        // Clear from GlassFoodDisplay (food display)
                        GlassFoodDisplay display = originalParent.GetComponentInParent<GlassFoodDisplay>();
                        if (display != null)
                        {
                            Debug.Log($"{heldDish.name} picked up from food display.");
                            // Optionally you can implement a ClearDisplaySlot if needed
                        }
                    }
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

                                spawner.SetDishAt(spawner.spawnPoints.IndexOf(spawnPoint), heldDish);

                                Debug.Log($"{heldDish.name} placed on table.");
                                heldDish = null;
                                return;
                            }
                        }

                        Debug.Log("No free spawn point on the table.");
                    }
                }

                // PUT DISH IN FOOD DISPLAY
                else if (heldDish != null && clicked.CompareTag("FoodDisplay"))
                {
                    GlassFoodDisplay display = clicked.GetComponent<GlassFoodDisplay>();
                    if (display == null)
                    {
                        display = clicked.GetComponentInParent<GlassFoodDisplay>();
                    }

                    if (display != null)
                    {
                        Transform emptySlot = display.GetFirstEmptySlot();
                        if (emptySlot != null)
                        {
                            heldDish.transform.SetParent(emptySlot);
                            heldDish.transform.position = emptySlot.position;
                            heldDish.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

                            Collider col = heldDish.GetComponent<Collider>();
                            if (col) col.enabled = true;

                            Debug.Log($"{heldDish.name} placed in food display.");
                            heldDish = null;
                        }
                        else
                        {
                            Debug.Log("No empty slot in food display.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No GlassFoodDisplay found.");
                    }
                }



            }
        }
    }
}
