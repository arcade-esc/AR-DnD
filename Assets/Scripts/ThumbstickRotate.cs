using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ThumbstickRotate : MonoBehaviour
{
    [SerializeField] private InputActionReference thumbstickAction;
    [SerializeField] private float rotationSpeed = 90f; // Adjust this value to control the rotation speed

    private HighlightOnHover highlightOnHover; // Add this line

    private void Awake()
    {
        // Get the HighlightOnHover component from the same GameObject
        highlightOnHover = GetComponent<HighlightOnHover>(); // Add this line
    }

    private void OnEnable()
    {
        thumbstickAction.action.Enable();
    }

    private void OnDisable()
    {
        thumbstickAction.action.Disable();
    }

    void Update()
    {
        if (highlightOnHover != null && highlightOnHover.IsHighlighted) // Check if the object is highlighted
        {
            Vector2 thumbstickValue = thumbstickAction.action.ReadValue<Vector2>();
            transform.Rotate(Vector3.up, thumbstickValue.x * rotationSpeed * Time.deltaTime);
        }
    }
}
