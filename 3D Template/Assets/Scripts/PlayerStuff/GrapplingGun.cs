using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform guntip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    public KeyCode Pfire;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    private void Update()
    {

        if (Input.GetKeyDown(Pfire))
        {
            startGrapple();
        }
        else if (Input.GetKeyUp(Pfire))
        {
            StopGrapple();
        }
    }
    private void LateUpdate()
    {
        DrawRope();
    }
    void startGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) { 
        
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
        
    }
    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, guntip.position);
        lr.SetPosition(1, grapplePoint);
    }
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
    public bool IsGrappling()
    {
        return joint == null;
    }

    public Vector3 GetGrapplePoints()
    {
        return grapplePoint;
    }
}
