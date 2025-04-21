using Unity.VisualScripting;
using UnityEngine;

public class INventoryObject : MonoBehaviour
{
    public ItemAssets inventoryItem;
    private float distanced;
    private float distancebetweentarget = 3;
    public GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, distancebetweentarget) && Input.GetKeyDown(KeyCode.H))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                FindFirstObjectByType<Inventory>().AddItem(inventoryItem.inventoryItem, this.gameObject);
            }
        }
        //Destroy(this.gameObject);
    }    
    
}
