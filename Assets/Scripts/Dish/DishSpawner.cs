using System.Collections.Generic;
using UnityEngine;

public class DishSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    private List<GameObject> spawnedDishes;

    private void Awake()
    {
        // Initialize the spawnedDishes list with nulls
        spawnedDishes = new List<GameObject>(new GameObject[spawnPoints.Count]);
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
}
