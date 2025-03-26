using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildObject : MonoBehaviour
{

    public bool foundation;
    public List<Collider> col = new List<Collider>();
    public Material Green;
    public Material Red;
    public bool IsBuildable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && foundation)
        {
            col.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 && foundation)
        {
            col.Remove(other);
        }
    }

    private void Update()
    {
        changeColor();
    }

    public void changeColor()
    {
        if (col.Count == 0)
            IsBuildable = true;
        else
            IsBuildable = false;

        if(IsBuildable)
        {
            foreach(Transform child in this.transform)
            {
                child.GetComponent<Renderer>().material = Green;
            }
        }
        else
        {
            foreach (Transform child in this.transform)
            {
                child.GetComponent<Renderer>().material = Red;
            }
        }
    }
}
