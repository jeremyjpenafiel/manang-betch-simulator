using System.Collections.Generic;
using UnityEngine;

public class MaterialEmissionToggle : MonoBehaviour
{
    [Tooltip("List of objects with emissive materials (only element 0 will be affected)")]
    public List<Renderer> targetRenderers;

    public Color emissionColor = Color.white;
    private bool emissionOn = false;
    private List<Material> materials = new List<Material>();

    void Start()
    {
        if (targetRenderers == null || targetRenderers.Count == 0)
        {
            Debug.LogWarning("No renderers assigned to MaterialEmissionToggle.");
            return;
        }

        foreach (Renderer rend in targetRenderers)
        {
            if (rend != null)
            {
                Material mat = rend.materials[0]; // Only affect element 0
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.black); // Start off
                materials.Add(mat);
            }
        }
    }


    public void ToggleEmission()
    {
        emissionOn = !emissionOn;

        foreach (Material mat in materials)
        {
            if (emissionOn)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", emissionColor);
            }
            else
            {
                mat.DisableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.black);
            }
        }
    }
}
