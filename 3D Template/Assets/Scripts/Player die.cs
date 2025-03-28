using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerdie : MonoBehaviour
{
    public GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Destoryer")
        {

            this.gameObject.SetActive(false);

            SceneManager.LoadScene("GameOver");
        }

    }
}

