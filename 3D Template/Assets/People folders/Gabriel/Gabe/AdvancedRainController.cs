using UnityEngine;
using System.Collections;

public class AdvancedRainController : MonoBehaviour
{
    public ParticleSystem rainParticle;
    public Light directionalLight;
    public AudioSource rainSound;
    public AudioSource thunderSound;

    public Material[] wetMaterials; // Materials that get wet over time

    public float thunderChance = 0.2f; // 20% chance per thunder check
    private bool isRaining = true;
    private float wetnessLevel = 0f;

    void Start()
    {
        if (rainParticle == null) rainParticle = GetComponent<ParticleSystem>();
        if (rainSound != null) rainSound.loop = true;

        StartCoroutine(ThunderRoutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleRain();
        }

        if (isRaining)
        {
            IncreaseWetness();
        }
        else
        {
            DecreaseWetness();
        }
    }

    public void ToggleRain()
    {
        isRaining = !isRaining;

        if (isRaining)
        {
            rainParticle.Play();
            if (rainSound != null) rainSound.Play();
            if (directionalLight != null) directionalLight.intensity = 0.5f; // Darken environment
        }
        else
        {
            rainParticle.Stop();
            if (rainSound != null) rainSound.Stop();
            if (directionalLight != null) directionalLight.intensity = 1.0f; // Restore light
        }
    }

    void IncreaseWetness()
    {
        wetnessLevel = Mathf.Lerp(wetnessLevel, 1f, Time.deltaTime * 0.1f);
        foreach (Material mat in wetMaterials)
        {
            mat.SetFloat("_Wetness", wetnessLevel);
        }
    }

    void DecreaseWetness()
    {
        wetnessLevel = Mathf.Lerp(wetnessLevel, 0f, Time.deltaTime * 0.1f);
        foreach (Material mat in wetMaterials)
        {
            mat.SetFloat("_Wetness", wetnessLevel);
        }
    }

    IEnumerator ThunderRoutine()
    {
        while (true)
        {
            if (isRaining && Random.value < thunderChance) // 20% chance
            {
                FlashLightning();
                yield return new WaitForSeconds(Random.Range(0.5f, 2f)); // Delay for thunder sound
                if (thunderSound != null) thunderSound.Play();
            }
            yield return new WaitForSeconds(Random.Range(5f, 15f)); // Check every 5-15 seconds
        }
    }

    void FlashLightning()
    {
        if (directionalLight != null)
        {
            StartCoroutine(LightningEffect());
        }
    }

    IEnumerator LightningEffect()
    {
        directionalLight.intensity = 2f;
        yield return new WaitForSeconds(0.1f);
        directionalLight.intensity = 0.5f;
    }
}