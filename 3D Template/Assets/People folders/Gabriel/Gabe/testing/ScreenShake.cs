using UnityEngine;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;
using Unity.Cinemachine;

public class ScreenShake : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float force = 1f)
    {
        impulseSource.GenerateImpulse(force);
    }
}