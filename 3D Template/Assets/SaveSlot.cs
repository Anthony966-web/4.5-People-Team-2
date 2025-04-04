using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
    public string SlotKey = "Slot";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToSlot()
    {
        SaveLoad.SlotKey = SlotKey;
        SceneManager.LoadScene("Landon");
        
    }
}
