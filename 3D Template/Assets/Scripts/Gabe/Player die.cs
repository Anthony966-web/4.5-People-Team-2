using UnityEngine;

public class Playerdie : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Destoryer")
        {
 
            Destroy(player.gameObject);
        }
    }

}

