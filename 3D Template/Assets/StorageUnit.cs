using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorageUnit : MonoBehaviour
{
    public bool PlayerInRange;

    [SerializeField] public List<ItemAssets> Items;

    private GameObject Text;

    //public bool IsOpen;

    public float OriMoveSpeed;

    public enum UnitSize
    {
        Small,
        Medium,
        Large,
        ExtraLarge,
        GodSize
    }

    public void Start()
    {
        OriMoveSpeed = PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().moveSpeed;
    }

    public UnitSize ThisUnitSize;

    void Update()
    {
        if (PlayerInRange || Text == null)
        {
            Text = GameObject.Find("Canvas").transform.Find("CrossHair").transform.Find("ItemFile").gameObject;
        }

        float distance = Vector3.Distance(PlayerState.Instance.playerBody.transform.position, transform.position);

        if (distance < 5f && !PlacementSystem.Instance.inPlacementMode)
        {
            PlayerInRange = true;
            Text.gameObject.SetActive(true);
            Text.GetComponent<TMP_Text>().text = "Open " + ThisUnitSize + "Chest [E]";
        }
        else
        {
            PlayerInRange = false;
            Text.gameObject.SetActive(false);
        }

        //if (!PlayerInRange && !StorageManager.Instance.IsOpen)
        //{
        //    StorageManager.Instance.StorageUnitSmallUI.SetActive(false);
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //    StorageManager.Instance.IsOpen = false;
        //    return;
        //}

        if (PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Check();
        }

        if(StorageManager.Instance.IsOpen)
        {
            PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().moveSpeed = 0;
            PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().enabled = false;
        }
        else
        {
            PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().enabled = true;
            PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().moveSpeed = OriMoveSpeed;
        }
    }

    public void Check()
    {
        if (!StorageManager.Instance.IsOpen)
        {
            //print("Works");
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            StorageManager.Instance.OpenBox(this);
            InventorySystem.Instance.isOpen = true;
            StorageManager.Instance.IsOpen = true;
            return;
        }
        else if (StorageManager.Instance.IsOpen)
        {
            StorageManager.Instance.CloseBox();
            InventorySystem.Instance.isOpen = false;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            StorageManager.Instance.IsOpen = false;
            return;
        }
    }
}
