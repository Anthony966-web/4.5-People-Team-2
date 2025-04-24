using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    private Slider slider;
    public TMP_Text hungerCounter;

    public GameObject playerState;

    private float currentHunger, maxHunger;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        currentHunger = playerState.GetComponent<PlayerState>().currentHunger;
        maxHunger = playerState.GetComponent<PlayerState>().maxHunger;

        float fillValue = currentHunger / maxHunger;
        slider.value = fillValue;

        //hungerCounter.text = "Hunger: " + currentHunger + "/" + maxHunger;
        //hungerCounter.text = "Hunger: " + currentHunger + "%";
        hungerCounter.text = "Hunger: " + Mathf.RoundToInt(fillValue * 100f) + "%";
    }
}
