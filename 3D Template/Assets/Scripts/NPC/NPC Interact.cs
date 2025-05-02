using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    public bool player_detection = false;

    private void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.F))
        {
            print("SIGMA!");
            // Open Shop UI
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