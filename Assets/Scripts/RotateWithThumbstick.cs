using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RotateWithThumbstickQuaternions : MonoBehaviour
{
    public InputActionProperty thumbstickInput;
    public float rotationSpeed = 100.0f;
    public float deadzone = 0.1f;
    public float smoothFactor = 0.1f;

    private XRGrabInteractable grabInteractable; // Reference to the XR Grab Interactable component
    private Quaternion targetRotation;
    private bool isGrabbed = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        thumbstickInput.action.Enable();

        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        thumbstickInput.action.Disable();

        // Unsubscribe from grab events
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        targetRotation = transform.rotation;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

    void Update()
    {
        if (!isGrabbed) return;

        Vector2 inputVector = thumbstickInput.action.ReadValue<Vector2>();

        if (inputVector.magnitude < deadzone)
        {
            inputVector = Vector2.zero;
        }
        else
        {
            Quaternion yaw = Quaternion.AngleAxis(inputVector.x * rotationSpeed * Time.deltaTime, Vector3.up);
            Quaternion pitch = Quaternion.AngleAxis(-inputVector.y * rotationSpeed * Time.deltaTime, Vector3.right);

            targetRotation = targetRotation * yaw * pitch;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothFactor);
        }
    }
}
