using System.Collections.Generic;
using UnityEngine;

public class VisibilityManager : MonoBehaviour
{
    public float checkInterval = 0.2f; // How often to check (in seconds)
    public float maxVisibleDistance = 100f; // Max range for objects to be considered
    private Camera mainCamera;
    private List<Optimizer> objectsToCheck = new List<Optimizer>();

    void Start()
    {
        mainCamera = Camera.main;

        // Find all objects with the Optimizer script
        Optimizer[] foundObjects = FindObjectsOfType<Optimizer>();
        objectsToCheck.AddRange(foundObjects);

        // Start checking the visibility at regular intervals
        InvokeRepeating(nameof(CheckVisibility), 0f, checkInterval);
    }

    void CheckVisibility()
    {
        // Check the distance of all objects from the camera
        foreach (var obj in objectsToCheck)
        {
            if (obj == null) continue;

            // Calculate the distance between the camera and the object
            float distance = Vector3.Distance(mainCamera.transform.position, obj.transform.position);

            // If the object is within the max visible distance, activate it, otherwise deactivate it
            bool shouldBeActive = distance <= maxVisibleDistance;
            obj.gameObject.SetActive(shouldBeActive);
        }
    }

    // This method will draw the range of the visibility check in the Scene view.
    private void OnDrawGizmos()
    {
        if (mainCamera == null)
            return;

        // Set the gizmo color to a semi-transparent red
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f); // Red with transparency

        // Draw a wire sphere to represent the max visible distance
        Gizmos.DrawWireSphere(mainCamera.transform.position, maxVisibleDistance);
    }
}
