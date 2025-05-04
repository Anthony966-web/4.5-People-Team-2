using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SaveSlot : MonoBehaviour
{
   public Button button;
    public TextMeshProUGUI buttonText;

    public int slotNumber;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.Find("text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    public void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (IsSlotEmpty(slotNumber))
            {
                 SaveManager.Instance.SaveGame(slotNumber);

                DateTime dt = DateTime.Now;
                string time = dt.ToString("yyyy-mm-dd HH:mm");

                buttonText.text = "Saved Game " + slotNumber + " | " + time;

                DeselectButton();
            }
            else
            {
                // DisplayOverrideWarning
            }

        }
        );
    }


    private void Update()
    {
        if (IsSlotEmpty(slotNumber))
        {
            buttonText.text = "Empty";
        }
        else
        {
            buttonText.text = PlayerPrefs.GetString("Slot" + slotNumber + "Description"); // 25:40
        }
    }


    private bool IsSlotEmpty(int slotNumber)
    {
        if (SaveManager.Instance.DoesFileExists(slotNumber))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void DeselectButton()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
