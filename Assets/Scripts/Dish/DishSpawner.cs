using System.Collections.Generic;
using UnityEngine;

public class DishSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    private List<GameObject> spawnedDishes;

    private void Awake()
    {
        spawnedDishes = new List<GameObject>(new GameObject[spawnPoints.Count]);

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnPoint dsp = spawnPoints[i].gameObject.AddComponent<SpawnPoint>();
            dsp.Initialize(this, i);
        }
    }

    public bool TrySpawnDish(GameObject dishPrefab)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnedDishes[i] == null)
            {
                GameObject dishInstance = Instantiate(dishPrefab, spawnPoints[i].position, Quaternion.identity);
                spawnedDishes[i] = dishInstance;
                dishInstance.transform.SetParent(spawnPoints[i]);
                return true;
            }
        }

        Debug.Log("All spawn points are occupied. Cannot spawn new dish.");
        return false;
    }

    public void TryPlaceHeldDish(int spawnIndex)
    {
        if (spawnedDishes[spawnIndex] != null)
        {
            Debug.Log("Spawn point is already occupied.");
            return;
        }

        GameObject bean = GameObject.Find("Bean(Clone)");
        if (bean == null) return;

        Transform hand = bean.transform.Find("Hand");
        if (hand == null || hand.childCount == 0) return;

        Transform heldDish = hand.GetChild(0);
        if (heldDish == null) return;

        heldDish.SetParent(spawnPoints[spawnIndex]);
        heldDish.position = spawnPoints[spawnIndex].position;
        heldDish.rotation = Quaternion.identity;

        Collider col = heldDish.GetComponent<Collider>();
        if (col) col.enabled = true;

        Rigidbody rb = heldDish.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        DishBehavior dishBehavior = heldDish.GetComponent<DishBehavior>();
        if (dishBehavior != null)
            dishBehavior.SetHeld(false);

        spawnedDishes[spawnIndex] = heldDish.gameObject;

        Debug.Log("Dish placed back on spawn point.");
    }

    public void ClearDishFromSpawn(GameObject dish)
    {
        for (int i = 0; i < spawnedDishes.Count; i++)
        {
            if (spawnedDishes[i] == dish)
            {
                spawnedDishes[i] = null;
                return;
            }
        }
    }
}
