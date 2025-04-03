using UnityEngine;

public class TimeStop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Time.timeScale = 0.2f;
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            Time.timeScale = 1;
        }
    }
}
