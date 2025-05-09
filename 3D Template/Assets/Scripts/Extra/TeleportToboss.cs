using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToboss : MonoBehaviour
{
    [SerializeField] private string bossfightSceneName = "Bossfight";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player touches the item
        if (other.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(bossfightSceneName))
            {
                SceneManager.LoadScene("Boss Room");
            }
            else
            {
                Debug.LogError("Bossfight scene name is not set!");
            }
        }
    }
}
