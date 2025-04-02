using UnityEngine;

public class Swimming : MonoBehaviour
{

    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded;


    private void Update()
    {
        HandleJump();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isGrounded = false;
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                
            }
        }
    }
}
