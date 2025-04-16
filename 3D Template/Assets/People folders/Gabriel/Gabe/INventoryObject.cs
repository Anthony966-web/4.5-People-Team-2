using UnityEngine;

public class INventoryObject : MonoBehaviour
{
    public ItemAssets inventoryItem;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<Inventory>().AddItem(inventoryItem.inventoryItem, this.gameObject);
            //Destroy(this.gameObject);
        }
    }
}
