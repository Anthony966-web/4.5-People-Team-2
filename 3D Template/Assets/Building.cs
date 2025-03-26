using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform cam;
    private Vector3 currentPos;
    public Transform currentBuildTransform;
    public RaycastHit hit;
    public LayerMask layer;
    public float offset = 1.0f;
    public float gridSize = 1.0f;
    public bool IsBuilding;

    public RandomPlacer randomPlacer;

    void Update()
    {
        if(IsBuilding && currentBuildTransform != null)
        {
            StartBuild();
        }
    }

    public void ChangeCurrentBuilding()
    {
        if(randomPlacer.CurrentBuild.gameObject != null)
        {
            GameObject build = Instantiate(randomPlacer.CurrentBuild.gameObject, currentPos, Quaternion.identity);
            currentBuildTransform = build.transform;
        }
    }

    public void StartBuild()
    {
        if(Physics.Raycast(cam.position, cam.forward, out hit, 10, layer))
        {
            if (hit.transform != this.transform)
                ShowBuild(hit);
        }
    }

    public void ShowBuild(RaycastHit hit2)
    {
        currentPos = hit2.point;
        currentPos -= Vector3.one* offset;
        currentPos /= gridSize;
        currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
        currentPos *= gridSize;
        currentPos += Vector3.one * offset;
        currentBuildTransform.position = currentPos;

        if (Input.GetMouseButtonDown(0))
        {
            currentBuildTransform = null;
        }
    }
}
