using UnityEngine;
using FoodSystem;

public class BeanInteraction : MonoBehaviour
{
    private GameObject heldDish = null;
    private GameObject heldTray = null;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject trayPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clicked = hit.collider.gameObject;

                if (heldDish == null && heldTray == null)
                {
                    if (clicked.CompareTag("Dish"))
                    {
                        TryPickUpDish(clicked);
                    }
                    else if (clicked.CompareTag("TrayPlate"))
                    {
                        // TryPickUpTray(clicked);
                        TryPickUpTray();
                    }
                }
                else if (heldDish != null)
                {
                    if (clicked.CompareTag("Table"))
                    {
                        TryPlaceDishOnTable(clicked);
                    }
                    else if (clicked.CompareTag("FoodDisplay"))
                    {
                        TryPlaceDishInFoodDisplay(clicked);
                    }
                }
                else if (heldTray != null)
                {
                    if (clicked.CompareTag("Dish"))
                    {
                        TryServeDishToTray(clicked);
                    }
                    else if (clicked.CompareTag("RiceCooker"))
                    {
                        // TryServeRiceToTray(clicked);
                    }
                }

            }
        }
    }

    private void TryPickUpDish(GameObject dish)
    {
        Transform originalParent = dish.transform.parent;

        heldDish = dish;
        heldDish.transform.SetParent(hand);
        heldDish.transform.localPosition = Vector3.zero;
        heldDish.transform.localRotation = Quaternion.identity;

        Collider col = heldDish.GetComponent<Collider>();
        if (col) col.enabled = false;

        // From DishSpawner
        DishSpawner spawner = originalParent.GetComponentInParent<DishSpawner>();
        if (spawner != null)
        {
            spawner.ClearSpawnedDish(heldDish.transform);
            Debug.Log($"{heldDish.name} picked up from spawn table.");
            return;
        }

        // From FoodDisplay
        GlassFoodDisplay display = originalParent.GetComponentInParent<GlassFoodDisplay>();
        if (display != null)
        {
            Debug.Log($"{heldDish.name} picked up from food display.");
            FoodItemSlot foodItemSlot = originalParent.GetComponent<FoodItemSlot>();
            if (foodItemSlot != null)
            {
                foodItemSlot.SetFoodItem(null);
                Debug.Log("Cleared food item from display slot.");
            }
        }
    }

    private void TryPickUpTray()
    {
        if (heldTray == null)
        {
            // Clone the tray
            heldTray = Instantiate(trayPrefab);

            heldTray.transform.SetParent(hand);
            heldTray.transform.localPosition = Vector3.zero;
            heldTray.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

            Collider col = heldTray.GetComponent<Collider>();
            if (col) col.enabled = false;

            Debug.Log("Picked up a tray.");
        }
        else
        {
            Debug.LogWarning("Already holding a tray.");
        }
    }


    private void TryPlaceDishOnTable(GameObject table)
    {
        DishSpawner spawner = table.GetComponent<DishSpawner>() ?? table.GetComponentInParent<DishSpawner>();
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

    private void TryPlaceDishInFoodDisplay(GameObject displayObject)
    {
        GlassFoodDisplay display = displayObject.GetComponent<GlassFoodDisplay>() ?? displayObject.GetComponentInParent<GlassFoodDisplay>();
        if (display != null)
        {
            Transform emptySlot = display.GetFirstEmptySlot();
            if (emptySlot != null)
            {
                heldDish.transform.SetParent(emptySlot);
                heldDish.transform.position = emptySlot.position;
                heldDish.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

                DishReference reference = heldDish.GetComponent<DishReference>();
                if (reference != null && reference.foodItem != null)
                {
                    FoodItemSlot foodItemSlot = emptySlot.GetComponent<FoodItemSlot>();
                    if (foodItemSlot != null)
                    {
                        foodItemSlot.SetFoodItem(reference.foodItem);
                        Debug.Log($"{reference.foodItem.FoodItemName} added to slot.");
                    }
                    else
                    {
                        Debug.LogWarning("No FoodItemSlot component found on the empty slot.");
                    }
                }
                else
                {
                    Debug.LogWarning("No DishReference or food item found on dish.");
                }

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

    private void TryServeDishToTray(GameObject dishObject)
    {
        TrayLayout layout = heldTray.GetComponent<TrayLayout>();
        if (layout != null && layout.HasServingSlotAvailable())
        {
            DishBehavior dish = dishObject.GetComponent<DishBehavior>();
            if (dish != null && dish.TryServe(out GameObject serving))
            {
                if (layout.TryPlaceOnTray(serving))
                {
                    Debug.Log($"Served 1 portion from {dish.name}. Remaining: {dish.GetRemainingQuantity()}");
                }
            }
        }
        else
        {
            Debug.Log("Tray is full. Cannot take more servings.");
        }
    }
    
    // private void TryServeRiceToTray(GameObject riceCooker)
    // {
    //     TrayLayout layout = heldTray.GetComponent<TrayLayout>();
    //     if (layout != null && layout.HasRiceSlotAvailable())
    //     {
    //         RiceCookerBehavior cooker = riceCooker.GetComponent<RiceCookerBehavior>();
    //         if (cooker != null && cooker.TryServe(out GameObject riceServing))
    //         {
    //             if (layout.TryPlaceRiceOnTray(riceServing))
    //             {
    //                 Debug.Log($"Served rice from {riceCooker.name}.");
    //             }
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("Tray's rice slot is already used.");
    //     }
    // }



}
