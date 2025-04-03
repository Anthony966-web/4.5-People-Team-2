using UnityEngine;

public class StairMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float stepHeight = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        CheckGround();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
          //  Jump();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);

        HandleStairs(moveDirection);
    }

    void HandleStairs(Vector3 moveDirection)
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, moveDirection);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f, groundLayer))
        {
            if (hit.point.y - transform.position.y < stepHeight)
            {
                rb.position += new Vector3(0, stepHeight, 0);
            }
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
}

