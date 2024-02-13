using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightOnHover : MonoBehaviour
{
    public Material defaultMaterial;
    public Material highlightMaterial;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        SetMaterial(defaultMaterial); // Set the default material initially
    }

    void OnEnable()
    {
        // Subscribe to hover events
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(HandleHoverEntered);
        interactable.hoverExited.AddListener(HandleHoverExited);
    }

    void OnDisable()
    {
        // Unsubscribe from hover events
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.RemoveListener(HandleHoverEntered);
        interactable.hoverExited.RemoveListener(HandleHoverExited);
    }

    private void HandleHoverEntered(HoverEnterEventArgs arg)
    {
        SetMaterial(highlightMaterial); // Change material on hover
    }

    private void HandleHoverExited(HoverExitEventArgs arg)
    {
        SetMaterial(defaultMaterial); // Revert material when not hovered
    }

    void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }
}
