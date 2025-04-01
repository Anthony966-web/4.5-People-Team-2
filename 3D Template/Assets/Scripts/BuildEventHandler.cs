using UnityEngine;
using UnityEngine.Events; // For Unity events (optional)
using System;
using System.Linq;

public class BuildEventHandler : MonoBehaviour
{
    // Declare the event

    RandomPlacer randomPlacer;

    void Start()
    {

    }

    void Update()
    {
        if (randomPlacer == null)
        {
            randomPlacer = FindObjectOfType<RandomPlacer>();
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log($"Clicked on: {hit.collider.gameObject.name}");
                    HandleObjectClicked(hit.collider.gameObject);
                }
                else
                {
                    randomPlacer.CurrentRemoveBuild = null;
                }
            }
            else
            {
                randomPlacer.CurrentRemoveBuild = null;
            }
        }
    }

    public void HandleObjectClicked(GameObject build)
    {
        string id = build.name;

        if (!string.IsNullOrEmpty(id) && randomPlacer.placeableObjects.Any(x => x.id == id))
        {
            Debug.Log($"Valid object clicked with ID: {id}");
            randomPlacer.CurrentRemoveBuild = build;
        }
        else
        {
            Debug.Log("Clicked object has no valid ID, ignoring.");
            randomPlacer.CurrentRemoveBuild = null;
        }
    }
}