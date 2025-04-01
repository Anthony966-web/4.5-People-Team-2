using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Day-Night Settings")]
    public Light sun;
    public float dayDuration = 60f; // Duration of a full day in seconds
    public Gradient skyColor; // Adjust this in the Inspector for smooth transitions

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        float timePercentage = (timeElapsed % dayDuration) / dayDuration; // Normalized time (0 to 1)

        // Rotate the sun for day-night effect
        float sunRotation = Mathf.Lerp(0, 360, timePercentage);
        sun.transform.rotation = Quaternion.Euler(sunRotation - 90f, 170f, 0f);

        // Change sky color based on time
        if (RenderSettings.ambientLight != null)
        {
            RenderSettings.ambientLight = skyColor.Evaluate(timePercentage);
        }
    }
}