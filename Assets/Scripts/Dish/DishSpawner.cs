using System.Collections.Generic;
using UnityEngine;

public class DishSpawner : MonoBehaviour
{
    [SerializeField] public List<Transform> spawnPoints;
    private List<GameObject> spawnedDishes;

    private void Awake()
    {
        spawnedDishes = new List<GameObject>();
        // Fill with null for each spawn point to match size
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnedDishes.Add(null);
        }
    }

    public bool TrySpawnDish(GameObject dishPrefab)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnedDishes[i] == null)
            {
                GameObject dishInstance = Instantiate(dishPrefab, spawnPoints[i].position, Quaternion.identity);
                dishInstance.transform.SetParent(spawnPoints[i]);

                spawnedDishes[i] = dishInstance;
                return true;
            }
        }

        Debug.Log("All spawn points are occupied. Cannot spawn new dish.");
        return false;
    }

    public void ClearSpawnedDish(Transform dishTransform)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnedDishes[i] != null && spawnedDishes[i].transform == dishTransform)
            {
                spawnedDishes[i] = null;
                return;
            }
        }

        // Optional fallback if you passed the spawn point
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i] == dishTransform)
            {
                spawnedDishes[i] = null;
                return;
            }
        }
    }



    public void SetDishAt(int index, GameObject dish)
    {
        if (index >= 0 && index < spawnedDishes.Count)
        {
            spawnedDishes[index] = dish;
        }
    }
}
