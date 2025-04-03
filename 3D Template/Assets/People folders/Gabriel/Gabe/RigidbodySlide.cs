using UnityEngine;
using System.Collections;

public class RigidbodySlide : MonoBehaviour
{
    public float slideSpeed = 8f;
    public float slideDuration = 0.6f;
    public float crouchHeight = 0.5f;
    public float normalHeight = 2f;
    public float slideCooldown = 1.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private CapsuleCollider col;
    private bool isSliding = false;
    private bool canSlide = true;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        CheckGround();
        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && !isSliding && canSlide && isGrounded && isMoving)
        {
            StartCoroutine(Slide());
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        canSlide = false;
        col.height = crouchHeight;
        Vector3 slideDirection = transform.forward;
        rb.linearVelocity = slideDirection * slideSpeed + Vector3.down * 2f;

        yield return new WaitForSeconds(slideDuration);

        col.height = normalHeight;
        isSliding = false;

        yield return new WaitForSeconds(slideCooldown);
        canSlide = true;
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
}