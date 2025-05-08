using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    public static NPCSystem Instance { get; set; }

    public bool player_detection = false;

    public GameObject InteractionText;

    public GameObject Shop;

    public bool IsOpen;

    public bool KeepActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(KeepActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(player_detection && !Shop.activeSelf)
            {
                // Open Shop UI
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Shop.SetActive(true);
                IsOpen = true;
            }
            else if(player_detection && Shop.activeSelf)
            {
                // Close Shop UI
                Cursor.lockState = CursorLockMode.Locked;//
                Cursor.visible = false;
                Shop.SetActive(false);
                IsOpen = false;
            }
        }
        else if(!player_detection)
        {
            // Close Shop UI
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Shop.SetActive(false);
            IsOpen = false;
        }

        if (player_detection)
        {
            if(!Shop.activeSelf)
            {
                InteractionText.SetActive(true);
                InteractionText.GetComponent<TMP_Text>().text = "Open Shop [E]";
            }

            if (Shop.activeSelf)
            {
                InteractionText.SetActive(true);
                InteractionText.GetComponent<TMP_Text>().text = "Close Shop [E]";
            }
        }
        else
        {
            InteractionText.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "PlayerOBJ")
            player_detection = true;
    }
    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
    }
}