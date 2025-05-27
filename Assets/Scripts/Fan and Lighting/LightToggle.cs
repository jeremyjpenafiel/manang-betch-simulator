using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    [Tooltip("Lights to toggle on/off")]
    public List<GameObject> lights;

    private bool lightsOn = true;



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
