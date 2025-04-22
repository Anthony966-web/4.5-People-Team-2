using UnityEngine;
using TMPro;

public class INventoryObject : MonoBehaviour
{
    public ItemAssets inventoryItem;
    //private float distanced;
    private float distancebetweentarget = 3;
    public GameObject player;

    public GameObject textContainer;



    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        textContainer = GameObject.Find("Canvas").transform.Find("CrossHair").transform.Find("ItemFile").gameObject;
    }
    public void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, distancebetweentarget))
        {
            Debug.Log("Pick Up UI");
            textContainer.SetActive(true);
            textContainer.GetComponent<TMP_Text>().text = inventoryItem.name + " x" + inventoryItem.inventoryItem.quantity + " [B]";

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
            textContainer.SetActive(false);
        }
    }

}
