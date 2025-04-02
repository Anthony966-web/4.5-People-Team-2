using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RandomPlacer : MonoBehaviour 
{
    public int MaxBuilds;

    public GameObject CurrentBuild;
    public GameObject CurrentRemoveBuild;

    public Identification[] placeableObjects;

    public List<SavableObjects> savableObjects = new List<SavableObjects>();

    public GameObject Parent;

    public Building buildingsScript;

    //public GameObject BuildGhosts;

    SaveLoad saveLoad;

    Transform parent;

    //public Transform cam;
    //public float BuildRangeMin;
    //public float BuildRangeMax;
    //private Vector3 currentPos;
    //public Transform currentBuildTransform;
    //public RaycastHit hit;
    //public LayerMask layer;
    //public float offset = 1.0f;
    //public float gridSize = 1.0f;
    //public bool IsBuilding;


    public void Awake()
    {
        Vector3 mousePosition = Input.mousePosition;
    }

    public void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        saveLoad = FindObjectOfType<SaveLoad>(); // Ensure SaveLoad is assigned

        if (saveLoad == null)
        {
            Debug.LogError("SaveLoad not found in the scene!");
            return;
        }

        saveLoad.Load(); // Now it's safe to call Load()

        if (saveLoad.savableObjects == null)
        {
            saveLoad.savableObjects = new List<SavableObjects>();
        }
    }

    //public void SelectPrefab(GameObject prefab)
    //{
    //    CurrentBuild = prefab;
    //    Debug.Log($"CurrentBuild set to: {CurrentBuild.name}");
    //}


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Save
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            saveLoad.Save();
            stopwatch.Stop();
            Debug.Log($"Save Time: {stopwatch.ElapsedMilliseconds} ms");
        }

        if (Input.GetKeyDown(KeyCode.L)) // Load
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            saveLoad.Load();
            stopwatch.Stop();
            Debug.Log($"Load Time: {stopwatch.ElapsedMilliseconds} ms");
        }

        if (Input.GetKeyDown(KeyCode.M)) // Press 'M' to check memory usage
        {
            Debug.Log($"Total Memory Used: {System.GC.GetTotalMemory(false) / 1024} KB");
        }

        //if (Input.GetKey(KeyCode.D))
        //{
        //    if (savableObjects.Count < MaxBuilds)
        //    {
        //        int rand = Random.Range(0, placeableObjects.Length);
        //        Vector3 randomPos = new Vector3(Random.Range(-50f, 50), 1, Random.Range(-50f, 50));

        //        GameObject obj = Instantiate(CurrentBuild); // Instantiate the selected prefab
        //        obj.transform.position = randomPos;
        //        obj.name = CurrentBuild.name;
        //        obj.transform.parent = Parent.transform;

        //        savableObjects.Add(new SavableObjects(obj.name, obj.transform.position, obj.transform.rotation));

        //        saveLoad.Save();
        //    }
        //}

        if (Input.GetKey(KeyCode.F))
        {
            if (CurrentRemoveBuild == null)
                return;

            RemoveBuild(CurrentRemoveBuild);
            CurrentRemoveBuild = null;
        }

    }

    //public void ShowBuild(RaycastHit hit2)
    //{
    //    currentPos = hit2.point;
    //    currentPos -= Vector3.one * offset;
    //    currentPos /= gridSize;
    //    currentPos = new Vector3(Mathf.Round(currentPos.x),Mathf.Round(currentPos.y),Mathf.Round(currentPos.z));
    //    currentPos *= gridSize;
    //    currentPos += Vector3.one * offset;
    //    CurrentBuild.transform.position = currentPos;
    //}

    //Building
    #region
    public void SelectBuild(GameObject BuildGameObject)
    {

        //Material material = BuildGameObject.GetComponent<Material>();
        CurrentBuild = BuildGameObject;
        //CurrentBuild.gameObject.transform.parent = Parent.transform;
        buildingsScript.ChangeCurrentBuilding();
        //int rand = Random.Range(0, placeableObjects.Length);
        //GameObject obj = Instantiate(CurrentBuild, currentPos, Quaternion.identity); // Instantiate the selected prefab
        //obj.name = CurrentBuild.name;
        //obj.transform.parent = BuildGhosts.transform;
        //currentBuildTransform = obj.transform;



        //savableObjects.Add(new SavableObjects(obj.name, obj.transform.position, obj.transform.rotation));

        //saveLoad.Save();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    print(CurrentBuild.name + "Placed");

        //    savableObjects.Add(new SavableObjects(obj.name, obj.transform.position, obj.transform.rotation));

        //    saveLoad.Save();
        //}
    }



    #endregion

    public void RemoveBuild(GameObject build)
    {
        print("K");

        // Find the matching object in the list
        SavableObjects toRemove = savableObjects.Find(obj =>
            obj.id == build.name &&
            obj.RetuernPosition() == build.transform.position &&
            obj.RetuernRotation() == build.transform.rotation
        );

        if (toRemove != null)
        {
            savableObjects.Remove(toRemove);
            Destroy(build.gameObject);
            saveLoad.Save();
        }
        else
        {
            Debug.LogWarning("Object not found in savableObjects list!");
        }
    }

    public void Reinstantiate()
    {
        for (int i = 0; i < savableObjects.Count; i++)
        {
            for (int z = 0; z < placeableObjects.Length; z++)
            {
                if (savableObjects[i].id == placeableObjects[z].id)
                {
                    GameObject obj = Instantiate(placeableObjects[z].prefab);
                    obj.transform.position = savableObjects[i].RetuernPosition();
                    obj.transform.rotation = savableObjects[i].RetuernRotation();
                    obj.name = placeableObjects[z].prefab.name;
                    obj.transform.parent = Parent.transform;
                    obj.GetComponent<BuildObject>().enabled = savableObjects[i].IsBuildObjectScript;
                    obj.GetComponent<Collider>().isTrigger = savableObjects[i].IsBuildObjectTrigger;

                    //Renderer renderer = obj.GetComponent<Renderer>();
                    //if (renderer != null)
                    //{
                    //    Material newMat = new Material(Shader.Find("Standard")); // Default material
                    //    newMat.name = savableObjects[i].materialName;
                    //    newMat.color = savableObjects[i].color;
                    //    renderer.material = newMat;
                    //}

                    print(obj.name);
                }
            }
        }
    }

    public void Uninstantiate()
    {
        if (Parent != null)
        {
            foreach (Transform child in Parent.transform)
            {
                Destroy(child.gameObject);
            }
        }
        else
            Debug.LogError("Nope");
    }
}

[System.Serializable]
public class SavableObjects
{
    public string id;
    public float px, py, pz;
    public float rx, ry, rz, rw;
    public bool IsBuildObjectTrigger;
    public bool IsBuildObjectScript;
    //public string materialName;
    //public Color color;

    public SavableObjects(string id, Vector3 position, Quaternion rotation, bool isBuildObjectTrigger, bool isBuildObjectScript)
    {
        this.id = id;

        px = position.x; py = position.y; pz = position.z;
        rx = rotation.x; ry = rotation.y; rz = rotation.z; rw = rotation.w;
        this.IsBuildObjectTrigger = isBuildObjectTrigger;
        this.IsBuildObjectScript = isBuildObjectScript;

        //if (material != null)
        //{
        //    this.materialName = material.name;
        //    this.color = material.color;
        //}
    }

    public Vector3 RetuernPosition()
    {
        Vector3 pos = new Vector3(px, py, pz);
        return pos;
    }

    public Quaternion RetuernRotation()
    {
        Quaternion rot = new Quaternion(rx, ry, rz, rw);
        return rot;
    }
}

[System.Serializable]
public class Identification
{
    public string id;
    public GameObject prefab;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id) || id != prefab.name)
        {
            id = prefab.name; // Automatically set ID to match object namef
        }
    }
}


//if (saveLoad.savableObjects.Count == 0) // If no saved objects, generate new ones
//{
//    for (int i = 0; i < randomToPlace; i++)
//    {
//        int rand = Random.Range(0, placeableObjects.Length);
//        Vector3 randomPos = new Vector3(Random.Range(-50f, 50), 1, Random.Range(-50f, 50));
//        GameObject obj = Instantiate(placeableObjects[rand].prefab);
//        obj.transform.position = randomPos;
//        obj.name = placeableObjects[rand].prefab.name;
//        obj.transform.parent = Parent.transform;

//        savableObjects.Add(new SavableObjects(placeableObjects[rand].id, obj.transform.position, obj.transform.rotation));

//        saveLoad.Save();
//    }
//}
