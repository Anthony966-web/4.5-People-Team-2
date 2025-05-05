using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorageUnit : MonoBehaviour
{
    public bool PlayerInRange;

    [SerializeField] public List<ItemAssets> Items;

    private GameObject Text;

    public enum UnitSize
    {
        Small,
        Medium,
        Large,
        ExtraLarge,
        GodSize
    }

    public UnitSize ThisUnitSize;

    void Update()
    {
        if (PlayerInRange || Text == null)
        {
            Text = GameObject.Find("Canvas").transform.Find("CrossHair").transform.Find("ItemFile").gameObject;
        }

        float distance = Vector3.Distance(PlayerState.Instance.playerBody.transform.position, transform.position);

        if (distance < 5f && PlacementSystem.Instance.inPlacementMode)
        {
            PlayerInRange = true;
            Text.gameObject.SetActive(true);
            Text.GetComponent<TMP_Text>().text = "Open " + ThisUnitSize + " [E]";
        }
        else
        {
            PlayerInRange = false;
            Text.gameObject.SetActive(false);
        }

        if(PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            print("Works");
        }
    }
}
