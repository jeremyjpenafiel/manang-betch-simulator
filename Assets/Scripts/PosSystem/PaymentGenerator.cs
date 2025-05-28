using System.Collections.Generic;
using UnityEngine;

public class PaymentGenerator : MonoBehaviour
{
    private readonly List<float> denominations = new List<float>
    {
        0.25f, 1f, 5f, 10f, 20f, 50f, 100f, 200f, 500f, 1000f
    };

    // Returns the next denomination greater than totalPrice
    public float GetPayment(float totalPrice)
    {
        denominations.Sort();

        foreach (float denom in denominations)
        {
            if (denom > totalPrice)
                return denom;
        }

        // If no higher denomination, just pay exact
        return totalPrice;
    }

    // Calculates change as a float value
    public float CalculateChange(float totalPrice)
    {
        float payment = GetPayment(totalPrice);
        return payment - totalPrice;
    }

    // Full process
    public void ProcessPayment(float totalPrice)
    {
        float payment = GetPayment(totalPrice);
        float change = CalculateChange(totalPrice);

        Debug.Log($"Total: {totalPrice}");
        Debug.Log($"NPC Paid: {payment}");
        Debug.Log($"Change Returned: {change}");
    }
}
