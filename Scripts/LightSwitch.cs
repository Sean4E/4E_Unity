using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Header("Light Reference")]
    public Light targetLight;

    [Header("Optional")]
    public Renderer emissiveRenderer;
    public Color emissiveColor = Color.yellow;
    public float emissiveIntensity = 2f;

    private bool isOn;
    private Material emissiveMaterial;

    void Start()
    {
        if (targetLight != null)
            isOn = targetLight.enabled;

        if (emissiveRenderer != null)
            emissiveMaterial = emissiveRenderer.material;

        UpdateEmissive();
    }

    // Call this from the Interactable's OnInteract event
    public void ToggleLight()
    {
        isOn = !isOn;

        if (targetLight != null)
            targetLight.enabled = isOn;

        UpdateEmissive();
    }

    void UpdateEmissive()
    {
        if (emissiveMaterial == null) return;

        if (isOn)
            emissiveMaterial.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
        else
            emissiveMaterial.SetColor("_EmissionColor", Color.black);
    }
}
