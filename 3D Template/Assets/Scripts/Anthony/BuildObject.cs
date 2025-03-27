using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildObject : MonoBehaviour
{

    public bool foundation;
    public List<Collider> col = new List<Collider>();
    public Material Green;
    public Material Red;
    public Material MainMaterial;
    public bool IsBuildable;

    void OnTriggerEnter(Collider other)
    {
        print("Work1");
        if (other.gameObject.layer == 8 && foundation)
        {
            print("Work2");
            col.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        print("Work3");
        if (other.gameObject.layer == 8 && foundation)
        {
            print("Work4");
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
