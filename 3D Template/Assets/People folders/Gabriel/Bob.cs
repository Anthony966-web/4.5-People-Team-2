using UnityEngine;

public class Bob : MonoBehaviour
{

    public Transform cameraTransform;   // Assign the Camera here
    public Rigidbody playerRb;          // Assign the Player's Rigidbody
    public float bobSpeed = 2.5f;       // Speed of bobbing
    public float bobAmount = 0.05f;     // Intensity of bobbing
    public float swayAmount = 1.5f;     // Rotation sway effect
    public float sideTiltAmount = 2f;   // Camera tilt angle when strafing
    public float smoothFactor = 8f;     // Smoothing transition speed

    private float bobTimer = 0f;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    void Start()
    {
        if (!playerRb)
            playerRb = GetComponentInParent<Rigidbody>(); // Auto-assign if not set

        originalCameraPosition = cameraTransform.localPosition;
        originalCameraRotation = cameraTransform.localRotation;
    }

    void Update()
    {
        Vector3 velocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
        float sideMovement = playerRb.linearVelocity.x; // Get sideways movement

        if (velocity.magnitude > 0.1f) // Check if the player is moving
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmount;
            float swayOffset = Mathf.Cos(bobTimer) * swayAmount;
            float sideTilt = -sideMovement * sideTiltAmount; // Negative to tilt correctly

            // Apply bobbing and swaying effects
            Vector3 targetPosition = originalCameraPosition + new Vector3(0, bobOffset, 0);
            Quaternion targetRotation = originalCameraRotation * Quaternion.Euler(swayOffset, sideTilt, swayOffset * 0.5f);

            // Smooth transition
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetPosition, Time.deltaTime * smoothFactor);
            cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, targetRotation, Time.deltaTime * smoothFactor);
        }
        else
        {
            // Reset camera smoothly when player stops moving
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, originalCameraPosition, Time.deltaTime * smoothFactor);
            cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, originalCameraRotation, Time.deltaTime * smoothFactor);
            bobTimer = 0f;
        }
    }
}
