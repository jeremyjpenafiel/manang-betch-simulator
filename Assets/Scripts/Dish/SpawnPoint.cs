using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private DishSpawner spawner;
    private int index;

    public void Initialize(DishSpawner spawnerRef, int spawnIndex)
    {
        spawner = spawnerRef;
        index = spawnIndex;
    }

    private void OnMouseDown()
    {
        spawner.TryPlaceHeldDish(index);
    }
}
