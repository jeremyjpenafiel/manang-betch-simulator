using System;
using UnityEngine;
using FoodSystem;
using Order;
using PosSystem;

public class BeanInteraction : MonoBehaviour
{
    private GameObject heldDish = null;
    private GameObject heldTray = null;
    private Tray tray;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject trayPrefab;
    [SerializeField] private PosView posView;


    public event Action<FoodItem> OnFoodAddToTray;
    public static event Action OnTrayPlacedInPaymentSection;

    private void Awake()
    {
        tray = GetComponent<Tray>();
        posView = FindObjectOfType<PosView>();
        if (posView == null)
        {
            Debug.LogError("PosView not found in the scene.");
        }
    }


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
                        if (clicked.CompareTag("Dish"))
                        {
                            // Check if this is the rice cooker by name
                            if (clicked.name.Equals("RiceCooker", StringComparison.OrdinalIgnoreCase))
                            {
                                // Optionally handle rice cooker dish click here
                                Debug.Log("RiceCooker dish clicked.");
                                // You can call a method here if needed, e.g. TryServeRiceToTray(clicked);
                            }
                            else
                            {
                                TryPickUpDish(clicked);
                            }
                        }

                    }
                    else if (clicked.CompareTag("TrayPlate"))
                    {
                        // TryPickUpTray(clicked);
                        TryPickUpTray();
                    }
                    else if (clicked.CompareTag("PaymentSection"))
                    {
                        TryPickUpTray();
                    }
                    else if (clicked.CompareTag("RiceCooker"))
                    {
                        // TryServeRiceToTray(clicked);
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
                else if (heldTray != null && (clicked.CompareTag("Dish")))
                {
                    TryServeDishToTray(clicked);
                }
                else if (heldTray != null && clicked.CompareTag("PaymentSection"))
                {
                    // Handle payment section interaction
                    Debug.Log("Interacted with payment section while holding a tray.");
                    // Chheck order agen 
                    if (OrderChecker.Instance.CheckOrder(tray.Dish, tray.Rice))
                    {
                        //place order
                        TryPlaceDishOnPaymentSection(clicked);
                        Debug.Log("Order placed successfully.");
                    }
                }
                else if (heldTray != null && clicked.CompareTag("RiceCooker"))
                {
                    // TryServeRiceToTray(clicked);
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
                foodItemSlot.FoodItem = null;
                Debug.Log("Cleared food item from display slot.");
            }
        }
    }

    private void TryPickUpTray()
    {
        if (heldTray == null)
        {
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
        if (display == null)
        {
            Debug.LogWarning("No GlassFoodDisplay found.");
            return;
        }

        Transform emptySlot = display.GetFirstEmptySlot();
        if (emptySlot == null)
        {
            Debug.Log("No empty slot in food display.");
            return;
        }

        heldDish.transform.SetParent(emptySlot);
        heldDish.transform.position = emptySlot.position;
        heldDish.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        DishReference reference = heldDish.GetComponent<DishReference>();
        if (reference == null || reference.foodItem == null)
        {
            Debug.LogWarning("No DishReference or food item found on dish.");
            return;
        }

        FoodItemSlot foodItemSlot = emptySlot.GetComponent<FoodItemSlot>();

        if (foodItemSlot != null)
        {
            foodItemSlot.FoodItem = reference.foodItem;
            Debug.Log($"{reference.foodItem.FoodItemName} added to slot.");
        }
        else
        {
            Debug.LogWarning("No FoodItemSlot component found on the empty slot.");
        }

        Collider col = heldDish.GetComponent<Collider>();
        if (col) col.enabled = true;

        Debug.Log($"{heldDish.name} placed in food display.");
        heldDish = null;
    }

    private void TryServeDishToTray(GameObject dishObject)
    {
        TrayLayout layout = heldTray.GetComponent<TrayLayout>();
        if (layout == null || !layout.HasAvailableSlot())
        {
            Debug.Log("Tray is full. Cannot take more servings.");
            return;
        }

        DishBehavior dish = dishObject.GetComponent<DishBehavior>();
        if (dish == null || !dish.TryServe(out GameObject serving)) return;

        if (!layout.TryPlaceOnTray(serving)) return;

        Debug.Log($"Served 1 portion from {dish.name}. Remaining: {dish.GetRemainingQuantity()}");

        FoodItem item = dishObject.GetComponent<DishReference>().foodItem;
        if (item) OnFoodAddToTray?.Invoke(item);


    }

    private void TryPlaceDishOnPaymentSection(GameObject paymentSectionTransform)
    {
        if (heldTray == null)
        {
            Debug.Log("You need to pick up a dish first.");
            return;
        }

        OnTrayPlacedInPaymentSection?.Invoke();
        Transform paymentSection = paymentSectionTransform.transform;
        heldTray.transform.SetParent(paymentSection);
        heldTray.transform.position = paymentSection.position;  
        tray.ResetOrder();
        posView.SetTrayInPaymentSection(heldTray); // Register it
        heldTray = null;
    }


    // private void TryServeRiceToTray(GameObject riceCookerObject)
    // {
    //     if (heldTray == null)
    //     {
    //         Debug.Log("You need to pick up a tray first.");
    //         return;
    //     }

    //     TrayLayout layout = heldTray.GetComponent<TrayLayout>();
    //     if (layout != null && layout.HasAvailableSlot())
    //     {
    //         DishReference dishRef = riceCookerObject.GetComponent<DishReference>();
    //         if (dishRef != null && dishRef.foodItem != null && dishRef.foodItem.foodPrefab != null)
    //         {
    //             GameObject riceServing = Instantiate(dishRef.foodItem.foodPrefab);
    //             if (layout.TryPlaceOnTray(riceServing))
    //             {
    //                 Debug.Log($"Served rice portion: {dishRef.foodItem.FoodItemName}");
    //             }
    //             else
    //             {
    //                 Destroy(riceServing);
    //                 Debug.Log("Tray is full. Could not place rice.");
    //             }
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Rice cooker missing DishReference or foodPrefab.");
    //         }
    //     }
    // }

}
