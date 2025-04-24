using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Slider slider;
    public TMP_Text healthCounter;

    public GameObject playerState;

    private float currentHealth, maxHealth;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;

        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;

        //healthCounter.text = "Health: " + currentHealth + "/" + maxHealth;
        //healthCounter.text = "Health: " + currentHealth + "%";
        healthCounter.text = "Health: " + Mathf.RoundToInt(fillValue * 100f) + "%";
    }
}
