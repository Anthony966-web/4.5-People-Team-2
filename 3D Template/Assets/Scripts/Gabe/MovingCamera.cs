using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public KeyCode Up = KeyCode.U, Third = KeyCode.T, first = KeyCode.F;
    [SerializeField] public GameObject FirstCamera;
    [SerializeField] public GameObject SecondCamera;
    [SerializeField] public GameObject ThirdCamera;
    
    public bool firstDisabled;
    public bool secondDisabled;
    public bool thirdDisabled;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))//second camera
        {
            FirstCamera.SetActive(false);
            ThirdCamera.SetActive(false);
            SecondCamera.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F))//first camera
        {
            SecondCamera.SetActive(false);
            ThirdCamera.SetActive(false);
            FirstCamera.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.U))//third camera
        {
            SecondCamera.SetActive(false);
            FirstCamera.SetActive(false);
            ThirdCamera.SetActive(true);
            
        }


    }
}
