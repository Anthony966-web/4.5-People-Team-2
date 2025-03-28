using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class AssigmentEvents : MonoBehaviour
{
    public UnityEvent onclick;
    public KeyCode KeyCode = KeyCode.Mouse1;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        { 
            var raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast))
            {
                onclick.Invoke();
            }
        }
    }
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
