using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TempPauseScript : MonoBehaviour
{
    #region ||---- Pause Menu ----||
    public GameObject PauseMenu;

    public bool isOpen;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isOpen)
            {
                isOpen = false;
            }
            else
            {
                isOpen = true;
            }
        }

        PauseMenu.SetActive(isOpen);
        if( isOpen )
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }
    }
    #endregion
}
