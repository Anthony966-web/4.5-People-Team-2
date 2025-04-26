using UnityEngine;
using TMPro;

public class INventoryObject : MonoBehaviour
{
    public ItemAssets inventoryItem;
    //private float distanced;
    private float distancebetweentarget = 1000;
    private GameObject player;

    private GameObject textContainer;


    public static GameObject CurrentTarget;

    public void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        textContainer = GameObject.Find("Canvas").transform.Find("CrossHair").transform.Find("ItemFile").gameObject;
    }
    public void Update()
    {   
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //print(ray);
        //print(gameObject);

        if (Physics.Raycast(ray, out RaycastHit hit, distancebetweentarget) && hit.collider.gameObject == this.gameObject)
        {
            Debug.DrawLine(player.transform.position, hit.collider.transform.position);

            print(hit.collider.name);
                CurrentTarget = this.gameObject;


                //FindFirstObjectByType<Inventory>().AddItem(inventoryItem.inventoryItem, this.gameObject);

                if (Input.GetKeyDown(KeyCode.E) && InventorySystem.Instance.isOpen == false && InventorySystem.Instance.IsDraggingItem == false)
                {
                    if (InventorySystem.Instance.CheckSlotIsAvailable(1))
                    {
                        InventorySystem.Instance.AddToInventory(inventoryItem, inventoryItem.Quantity);
                        DestroyImmediate(this.gameObject);
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

            //Debug.Log("Pick Up UI");
            textContainer.SetActive(true);
            textContainer.GetComponent<TMP_Text>().text = CurrentTarget.GetComponent<INventoryObject>().inventoryItem.ItemName + " [E]";
        }
        else
        {

            //Debug.Log("No Pick Up UI");
            textContainer.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        
    }

}
