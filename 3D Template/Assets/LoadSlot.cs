using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSlot : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;

    public int slotNumber;

    public void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
    }
}

// Make Sure Your Code Has No Error's Before Pushing!!!!!!!!!!!!!!!!! // Anthony