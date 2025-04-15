using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Worlds To Load")]
    public string _newGameWorld;
    private string WorldToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameWorld);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedWorld"))
        {
            WorldToLoad = PlayerPrefs.GetString("SavedWorld");
            //PlayerPrefs.SetString("SavedLevel", whateveryourlevelis)
            SceneManager.LoadScene(WorldToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
