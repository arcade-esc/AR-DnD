using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ThumbstickRotate : MonoBehaviour
{
    [SerializeField] private InputActionReference thumbstickAction;
    [SerializeField] private float rotationSpeed = 90f; // Adjust this value to control the rotation speed

    private void OnEnable()
    {
        // Enable the thumbstick action
        thumbstickAction.action.Enable();
    }

    private void OnDisable()
    {
        // Disable the thumbstick action
        thumbstickAction.action.Disable();
    }

    void Update()
    {
        // Read the Vector2 value from the thumbstick
        Vector2 thumbstickValue = thumbstickAction.action.ReadValue<Vector2>();

        // Rotate around the y-axis by the x component of the thumbstick value
        transform.Rotate(Vector3.up, thumbstickValue.x * rotationSpeed * Time.deltaTime);
    }
}
