using UnityEngine;

public class DishBehavior : MonoBehaviour
{
    [SerializeField] private int dishQuantity = 10;
    [SerializeField] private GameObject servingPrefab;

    public bool TryServe(out GameObject serving)
    {
        if (dishQuantity > 0)
        {
            dishQuantity--;
            serving = Instantiate(servingPrefab);
            return true;
        }

        serving = null;
        return false;
    }

    public int GetRemainingQuantity()
    {
        return dishQuantity;
    }
}
