using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSlot : MonoBehaviour
{
    private Button button;
    private TMP_Text buttonText;

    public int slotNumber;

    public void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (SaveManager.Instance.IsSlotEmpty(slotNumber))
        {
            buttonText.text = "No Data";
        }
        else
        {
            buttonText.text = PlayerPrefs.GetString("Slot" + slotNumber + "Description");
        }

    }

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (SaveManager.Instance.IsSlotEmpty(slotNumber) == false)
            {
                SaveManager.Instance.StartLoadedGame(slotNumber);
                SaveManager.Instance.DeselectButton();
            }
            else
            {
                // if empty do nothing
            }
        });
    }
}

