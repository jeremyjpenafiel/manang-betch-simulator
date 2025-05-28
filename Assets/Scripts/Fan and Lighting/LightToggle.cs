using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    [Tooltip("Lights to toggle on/off")]
    public List<GameObject> lights;

    private bool lightsOn = true;

    private void Update()
    {
        // Optional: You can add a key input to toggle lights for testing
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLights();
        }
    }

    public void ToggleLights()
    {
        lightsOn = !lightsOn;

        foreach (GameObject lightObj in lights)
        {
            if (lightObj != null)
                lightObj.SetActive(lightsOn);
        }
    }
}
