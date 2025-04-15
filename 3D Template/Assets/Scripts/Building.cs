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

    private Vector3 currentRot;

    public RandomPlacer randomPlacer;
    public SaveLoad saveLoad;
    public GameObject BuildParent;

    void Update()
    {
        if(IsBuilding && currentBuildTransform != null)
        {
            StartBuild();

            if (Input.GetMouseButtonDown(0))
            {
                Build();
            }
        }
    }

    public void ChangeCurrentBuilding()
    {
        if(randomPlacer.CurrentBuild.gameObject != null && currentBuildTransform == null)
        {
            GameObject build = Instantiate(randomPlacer.CurrentBuild.gameObject, currentPos, Quaternion.Euler(currentRot));
            currentBuildTransform = build.transform;
        }
    }

    public void StartBuild()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10, layer))
        {
            if (hit.transform != this.transform)
                ShowBuild(hit);
        }
    }

    public void ShowBuild(RaycastHit hit2)
    {
        currentPos = hit2.point;
        currentPos -= Vector3.one * offset;
        currentPos /= gridSize;
        currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
        currentPos *= gridSize;
        currentPos += Vector3.one * offset;
        currentBuildTransform.transform.position = currentPos;
        currentBuildTransform.gameObject.layer = 6;
        if(Input.GetKeyDown(KeyCode.R))
        {
            currentRot += new Vector3(0, 45, 0);
            currentBuildTransform.localEulerAngles = currentRot;
        }
    }

    public void Build()
    {
        BuildObject BO = currentBuildTransform.GetComponent<BuildObject>();
        if (BO.IsBuildable)
        {
            currentBuildTransform.name = currentBuildTransform.name.Replace("(Clone)", "").Trim();

            if (BO.sort == objectsorts.normal && BO.transform.name == "Wall")
            {
                Debug.Log(BO.transform.name);
                BO.transform.position = currentPos + new Vector3(0, BO.transform.localScale.y, 0) / 2;
            }
           

            //Build the object
            BO.GetComponent<Renderer>().material = BO.GetComponent<BuildObject>().MainMaterial;
            BO.GetComponent<Collider>().isTrigger = false;
            BO.GetComponent<BuildObject>().enabled = false;
            currentBuildTransform.transform.parent = BuildParent.transform;

            currentBuildTransform.gameObject.layer = 8;
            

            randomPlacer.savableObjects.Add(new SavableObjects(currentBuildTransform.name, currentBuildTransform.transform.position, currentBuildTransform.transform.rotation, currentBuildTransform.GetComponent<Collider>().isTrigger = false, currentBuildTransform.GetComponent<BuildObject>().enabled = false));

            //obj.transform.position = randomPos;
            //obj.name = CurrentBuild.name;
            //obj.transform.parent = Parent.transform;

            //savableObjects.Add(new SavableObjects(obj.name, obj.transform.position, obj.transform.rotation));

            saveLoad.Save();

            currentBuildTransform = null;
        }
        else
        {
            Destroy(currentBuildTransform.gameObject);
            saveLoad.Save();
            currentBuildTransform = null;
        }
    }
}
