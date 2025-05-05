using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public bool player_detection = false;

    private void Update()
    {
        if (player_detection == true)
        {
            SceneManager.LoadScene("Game1");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerOBJ")
            player_detection = true;
    }
    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
    }
}