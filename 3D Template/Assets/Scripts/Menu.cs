using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public static bool GameIsPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
    }
void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
}
