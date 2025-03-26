using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeBody : MonoBehaviour
{
    private bool isRewinding = false;

    public float recordTime = 5f;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
        }
    }
    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else {
            Record();
        }
    }
    void Record()
    {
        if (pointsInTime.Count >  Mathf.Round(recordTime / Time.fixedDeltaTime))  // 5 seconds
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
            pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
        
        }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
        StopRewind ();
        }
    }

    public void StartRewind()
    {
        isRewinding=true;
        rb.isKinematic = true;
    }
  public  void StopRewind()
    {
        isRewinding=false;
        rb.isKinematic = false;
    }
}
