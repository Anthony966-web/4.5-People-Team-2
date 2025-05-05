using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSlot : MonoBehaviour
{
    public Button button;
    //public TMPro.TextMeshProUFUI buttonText;

    public int slotNumber;

    public void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.Find("Text(TMP)").GetComponent<TextMeshProUGUI>();
    }
}
