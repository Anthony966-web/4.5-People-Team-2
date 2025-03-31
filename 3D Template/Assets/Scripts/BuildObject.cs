using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildObject : MonoBehaviour
{

    public List<Collider> col = new List<Collider>();
    public objectsorts sort;
    public Material Green;
    public Material Red;
    public Material MainMaterial;
    public bool IsBuildable;
    public BuildObject childcol;
    public bool Second;

    void OnTriggerEnter(Collider other)
    {
        print("Work1");
        if (other.gameObject.layer == 8)
        {
            print("Work2");
            col.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        print("Work3");
        if (other.gameObject.layer == 8)
        {
            print("Work4");
            col.Remove(other);
        }
    }

    private void Update()
    {
        if(!Second)
            changeColor();
    }

    public void changeColor()
    {

        if(sort == objectsorts.foundation)
        {
            if (col.Count == 0)
                IsBuildable = true;
            else
                IsBuildable = false;
        }
        else
        {
            //if (col.Count == 0 && childcol.col.Count > 0)
            //    IsBuildable = true;
            //else
            //    IsBuildable = false;
            //if(sort == objectsorts.normal)
            //{
            //    if (col.Contains(gameObject.GetComponent<BuildObject>().sort) == objectsorts.foundation)
            //        IsBuildable = true;
            //    else
            //        IsBuildable = false;
            //}
        }

        if (IsBuildable)
        {
            //foreach(Transform child in this.transform)
            //{
            //child.GetComponent<Renderer>().material = Green;
            this.GetComponent<Renderer>().material = Green;
            print("Green");
            //}
        }
        else
        {
            //foreach (Transform child in this.transform)
            //{
            this.GetComponent<Renderer>().material = Red;
            //child.GetComponent<Renderer>().material = Red;
            print("Red");
            //}
        }
    }
}

public enum objectsorts
{
    normal,
    foundation,
    floor
}
