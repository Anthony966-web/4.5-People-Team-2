using UnityEngine;
using TMPro;

public class INventoryObject : MonoBehaviour
{
    public ItemAssets inventoryItem;
    //private float distanced;
    private float distancebetweentarget = 3;
    public GameObject player;

    public GameObject textContainer;

    private static GameObject CurrentTarget;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        textContainer = GameObject.Find("Canvas").transform.Find("CrossHair").transform.Find("ItemFile").gameObject;
    }
    public void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, distancebetweentarget)&& hit.collider.gameObject == this.gameObject)
        {

                CurrentTarget = this.gameObject;


                //FindFirstObjectByType<Inventory>().AddItem(inventoryItem.inventoryItem, this.gameObject);

                if (Input.GetKeyDown(KeyCode.B))
                {
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
        else if (CurrentTarget == this.gameObject)
        {
            CurrentTarget = null;   
        }

        if (CurrentTarget)
        {

            Debug.Log("Pick Up UI");
            textContainer.SetActive(true);
            textContainer.GetComponent<TMP_Text>().text = inventoryItem.name + " x" + inventoryItem.inventoryItem.quantity + " [B]";
        }
        else
        {

            Debug.Log("No Pick Up UI");
            textContainer.SetActive(false);
        }
    }

}
