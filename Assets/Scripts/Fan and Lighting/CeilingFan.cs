using UnityEngine;

public class CeilingFan : MonoBehaviour
{
    [SerializeField] GameObject fanBlade; // Reference to the fan blade GameObject

    [Header("Rotation Settings")]
    public float rotationSpeed = 200f; // Degrees per second
    public bool isOn = true;

    void Update()
    {
        if (isOn)
        {
            fanBlade.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    // This will be called when you click on the GameObject with this script attached
    void OnMouseDown()
    {
        ToggleFan();
    }

    public void ToggleFan()
    {
        isOn = !isOn;
    }
}
