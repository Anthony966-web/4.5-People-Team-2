using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloading : MonoBehaviour
{
public void Title()
    {
        SceneManager.LoadScene("Title");
    }
    public void Maze()
    {
        SceneManager.LoadScene("Landon");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

        }
    }
}
