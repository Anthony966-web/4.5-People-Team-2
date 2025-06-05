using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class Study : MonoBehaviour
{
    public Rigidbody rb;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseDown();
       /// Transform clonedObject = Instantiate(
//projectilePrefab
///,
//transform.position
//,
//Quaternion.identity
///);
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // 0 = left mouse button
        {
            rb.AddForce(-transform.forward * 10, ForceMode.VelocityChange);
            rb.AddForce(transform.up * 10, ForceMode.VelocityChange);
        }
        //transform.position = Vector3.Nor(transform.position, homeSpot.transform.position, moveSpeed * Time.deltaTime);
    }

    public IEnumerator enumerator()
    {
        yield return new WaitForSeconds(1f);
        rb.AddForce(-transform.forward * 10, ForceMode.VelocityChange);
        rb.AddForce(transform.up * 10, ForceMode.VelocityChange);
    }

}
