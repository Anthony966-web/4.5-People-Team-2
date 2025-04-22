using Unity.VisualScripting;
using UnityEngine;

public class INventoryObject : MonoBehaviour
{
    public ItemAssets inventoryItem;
    //private float distanced;
    private float distancebetweentarget = 20;
    public GameObject player;



    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, distancebetweentarget))
        {
            Debug.Log("Pick Up UI");
            if (hit.collider.gameObject == this.gameObject && Input.GetKeyDown(KeyCode.B))
            {
                //FindFirstObjectByType<Inventory>().AddItem(inventoryItem.inventoryItem, this.gameObject);

                if (!InventorySystem.Instance.CheckIfFull())
                {
                    InventorySystem.Instance.AddToInventory(inventoryItem.name);
                    Destroy(this.gameObject);
                }
                else
                {
                    Debug.Log("Inventory Is Full");
                }
            }
        }
        else
        {
            Debug.Log("No Pick Up UI");
        }
    }

}
