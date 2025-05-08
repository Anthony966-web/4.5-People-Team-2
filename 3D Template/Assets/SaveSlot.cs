using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SaveSlot : MonoBehaviour
{
    private Button button;
    private TMP_Text buttonText;

    public int slotNumber;

    public GameObject AlertUI;
    Button yesButton;
    Button noButton;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();

        yesButton = AlertUI.transform.GetChild(1).GetComponent<Button>();
        noButton = AlertUI.transform.GetChild(2).GetComponent<Button>();
    }

    public void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (SaveManager.Instance.IsSlotEmpty(slotNumber))
            {
                SaveGameConfirm();
            }
            else
            {
                DisplayOverrideWarning();
            }
        }
        );
    }


    private void Update()
    {
        if (SaveManager.Instance.IsSlotEmpty(slotNumber))
        {
            buttonText.text = "Empty";
        }
        else
        {
            buttonText.text = PlayerPrefs.GetString("Slot" + slotNumber + "Description");
        }
    }

    public void DisplayOverrideWarning()
    {
        AlertUI.SetActive(true);

        yesButton.onClick.AddListener(() =>
        {
            SaveGameConfirm();

            AlertUI.SetActive(false);
        });

        noButton.onClick.AddListener(() =>
        {
            AlertUI.SetActive(false);
        });
    }

    private void SaveGameConfirm()
    {
        SaveManager.Instance.SaveGame(slotNumber);

        DateTime dt = DateTime.Now;
        string time = dt.ToString("yyyy-mm-dd HH:mm");

        string description = "Saved Game " + slotNumber + " | " + time;

        buttonText.text = description;

        PlayerPrefs.SetString("Slot" + slotNumber + "Description", description);

        SaveManager.Instance.DeselectButton();
    }
}
