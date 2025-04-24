using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToxicBar : MonoBehaviour
{
    private Slider slider;
    public TMP_Text toxicCounter;

    public GameObject playerState;

    private float currentToxicImmunity, maxToxicImmunity;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        currentToxicImmunity = playerState.GetComponent<PlayerState>().currentToxicImmunity;
        maxToxicImmunity = playerState.GetComponent<PlayerState>().maxToxicImmunity;

        float fillValue = currentToxicImmunity / maxToxicImmunity;
        slider.value = fillValue;

        //toxicCounter.text = "Toxic Immunity: " + currentToxicImmunity + "/" + maxToxicImmunity;
        //toxicCounter.text = "Toxic Immunity: " + currentToxicImmunity + "%";
        toxicCounter.text = "Toxic Immunity: " + Mathf.RoundToInt(fillValue * 100f) + "%";
    }
}
