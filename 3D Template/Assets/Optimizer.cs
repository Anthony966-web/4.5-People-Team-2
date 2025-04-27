using UnityEngine;

public class Optimizer : MonoBehaviour
{
    public Renderer targetRenderer;

    private void Awake()
    {
        targetRenderer = GetComponent<Renderer>();
    }
}
