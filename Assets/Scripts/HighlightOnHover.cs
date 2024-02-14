using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightOnHover : MonoBehaviour
{
    public Material defaultMaterial;
    public Material highlightMaterial;
    public bool IsHighlighted { get; private set; } = false;

    private Renderer[] renderers; // Array to hold all renderer components

    void Awake()
    {
        // Find all Renderer components in this object and its children, including MeshRenderers and SkinnedMeshRenderers
        renderers = GetComponentsInChildren<Renderer>(true);

        // Set to default material at start
        if (renderers.Length > 0)
        {
            SetMaterial(defaultMaterial);
        }
        else
        {
            Debug.LogError("No Renderer found on the object or its children!");
        }
    }

    void OnEnable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(HandleHoverEntered);
        interactable.hoverExited.AddListener(HandleHoverExited);
    }

    void OnDisable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.RemoveListener(HandleHoverEntered);
        interactable.hoverExited.RemoveListener(HandleHoverExited);
    }

    private void HandleHoverEntered(HoverEnterEventArgs arg)
    {
        SetMaterial(highlightMaterial); // Change to the highlight material on hover
        IsHighlighted = true;
    }

    private void HandleHoverExited(HoverExitEventArgs arg)
    {
        SetMaterial(defaultMaterial); // Revert to the default material when not hovered
        IsHighlighted = false;
    }

    // Method to change the object's material
    private void SetMaterial(Material material)
    {
        foreach (var renderer in renderers)
        {
            // Log for debugging
            Debug.Log("Applying material to: " + renderer.gameObject.name);

            // Create an array with the same length as the original materials array, filled with the new material
            Material[] newMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = material;
            }
            renderer.materials = newMaterials;
        }
    }
}
