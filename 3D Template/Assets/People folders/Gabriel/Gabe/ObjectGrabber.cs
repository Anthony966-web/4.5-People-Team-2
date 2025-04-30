using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    public Camera playerCamera;
    public float grabRange = 5f;
    public float throwForce = 10f;
    public Transform holdPosition;
    public LayerMask grabbableLayer;
    private Rigidbody grabbedObject;
    private bool isHoldingObject = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingObject)
            {
                TryGrabObject();
            }
            else
            {
                DropObject();
            }
        }

        if (isHoldingObject && Input.GetMouseButtonDown(0))
        {
            ThrowObject();
        }
    }

    void FixedUpdate()
    {
        if (isHoldingObject && grabbedObject != null)
        {
            MoveObject();
        }
    }

    void TryGrabObject()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, grabRange, grabbableLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                grabbedObject = rb;
                grabbedObject.useGravity = false;
                grabbedObject.linearDamping = 10;
                grabbedObject.transform.parent = holdPosition;
                isHoldingObject = true;
            }
        }
    }

    void MoveObject()
    {
        if (grabbedObject != null)
        {
            Vector3 moveDirection = (holdPosition.position - grabbedObject.transform.position);
            grabbedObject.linearVelocity = moveDirection * 10f;
        }
    }

    void DropObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            grabbedObject.linearDamping = 1;
            grabbedObject.transform.parent = null;
            grabbedObject = null;
            isHoldingObject = false;
        }
    }

    void ThrowObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            grabbedObject.linearDamping = 1;
            grabbedObject.transform.parent = null;
            grabbedObject.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);
            grabbedObject = null;
            isHoldingObject = false;
        }
    }
}