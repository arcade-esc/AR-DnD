using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapToPlane : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnObjectReleased);
    }

    private void OnDestroy()
    {
        grabInteractable.selectExited.RemoveListener(OnObjectReleased);
    }

    private void OnObjectReleased(SelectExitEventArgs args)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

        if (hits.Count > 0)
        {
            ARRaycastHit hit = hits[0];
            AlignObjectWithPlane(hit);
        }
    }

    private void AlignObjectWithPlane(ARRaycastHit hit)
    {
        // Calculate the closest point on the object's collider to the hit position
        Collider collider = GetComponent<Collider>();
        Vector3 closestPoint = collider.ClosestPoint(hit.pose.position);

        // Determine the translation needed to move that point to the hit position
        Vector3 translation = hit.pose.position - closestPoint;

        // Move the object by the calculated translation
        transform.position += translation;

        // Align the object's up vector with the plane's normal vector
        // Assuming that the plane's normal is 'up' in world space
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.pose.up) * transform.rotation;
    }
}
