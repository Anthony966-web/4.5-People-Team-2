//using UnityEngine;
//using UnityEngine.Rendering;

//public class CharacterMovement : MonoBehaviour
//{
//    public float moveSpeed = 5f;
//    public float sprintSpeed = 8f;
//    public float crouchSpeed = 2.5f;
//    public float jumpForce = 7f;
//    public float slideDistance = 15f;
//    public float sprintCooldown = 2f;
//    public float teleportCooldown = 2f;
//    public float maxStamina = 5f;
//    public Transform cameraTransform;
//    public float lookSensitivity = 2f;
//    public float slideZoomAmount = 0.8f;
//    public float slideZoomSpeed = 10f;
//    public float runFOVIncrease = 10f;
//    public float fovChangeSpeed = 5f;

//    private Rigidbody rb;
//    private bool isGrounded;
//    private bool canDoubleJump;
//    private bool isCrouching;
//    private bool canTeleport = true;
//    private Vector3 originalScale;
//    private float stamina;
//    private bool isSprinting;
//    private float rotationX = 0f;
//    private Camera playerCamera;
//    private float originalFOV;
//    private bool sprintOnCooldown = false;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        rb.freezeRotation = true;
//        originalScale = transform.localScale;
//        stamina = maxStamina;
//        playerCamera = cameraTransform.GetComponent<Camera>();
//        if (playerCamera != null)
//        {
//            originalFOV = playerCamera.fieldOfView;
//        }
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    void Update()
//    {
//        Move();
//        HandleJump();
//        HandleCrouch();
//        HandleSprint();
//        Teleport();
//        CameraControl();
//        HandleCameraFOV();
//    }

//    void Move()
//    {
//        float moveX = Input.GetAxis("Horizontal");
//        float moveZ = Input.GetAxis("Vertical");

//        Vector3 moveDir = transform.right * moveX + transform.forward * moveZ;
//        float currentSpeed = isSprinting ? sprintSpeed : (isCrouching ? crouchSpeed : moveSpeed);

//        rb.linearVelocity = new Vector3(moveDir.x * currentSpeed, rb.linearVelocity.y, moveDir.z * currentSpeed);
//    }

//    void HandleJump()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            if (isGrounded)
//            {
//                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
//                canDoubleJump = true;
//            }
//            else if (canDoubleJump)
//            {
//                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
//                canDoubleJump = false;
//            }
//        }
//    }

//    void HandleCrouch()
//    {
//        if (Input.GetKeyDown(KeyCode.LeftControl))
//        {
//            isCrouching = true;
//            transform.localScale = new Vector3(originalScale.x, originalScale.y / 2, originalScale.z);
//        }

//        else if (Input.GetKeyUp(KeyCode.LeftControl))
//        {
//            isCrouching = false;
//            transform.localScale = originalScale;
//        }
//    }

//    void HandleSprint()
//    {
//        if (!sprintOnCooldown && Input.GetKey(KeyCode.LeftShift) && stamina > 0)
//        {
//            isSprinting = true;
//            stamina -= Time.deltaTime;
//            if (stamina <= 0)
//            {
//                stamina = 0;
//                sprintOnCooldown = true;
//                Invoke("ResetSprintCooldown", sprintCooldown);
//            }
//        }
//        else
//        {
//            isSprinting = false;
//            if (stamina < maxStamina && !sprintOnCooldown)
//                stamina += Time.deltaTime;
//        }
//    }

//    void ResetSprintCooldown()
//    {
//        sprintOnCooldown = false;
//    }

//    void Teleport()
//    {
//        if (Input.GetKeyDown(KeyCode.C) && canTeleport)
//        {
//            canTeleport = false;
//            rb.AddForce(transform.forward * slideDistance, ForceMode.VelocityChange);
//            if (playerCamera != null)
//            {
//                StartCoroutine(SlideCameraEffect());
//            }
//            Invoke("ResetTeleport", teleportCooldown);
//        }
//    }

//    System.Collections.IEnumerator SlideCameraEffect()
//    {
//        float elapsedTime = 0f;
//        float targetFOV = originalFOV * slideZoomAmount;
//        while (elapsedTime < 0.2f)
//        {
//            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, elapsedTime * slideZoomSpeed);
//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }
//        yield return new WaitForSeconds(0.3f);
//        elapsedTime = 0f;
//        while (elapsedTime < 0.2f)
//        {
//            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, originalFOV, elapsedTime * slideZoomSpeed);
//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }
//        playerCamera.fieldOfView = originalFOV;
//    }

//    void ResetTeleport()
//    {
//        canTeleport = true;
//    }

//    void CameraControl()
//    {
//        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
//        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

//        transform.Rotate(Vector3.up * mouseX);
//        rotationX -= mouseY;
//        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
//        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
//    }

//    void HandleCameraFOV()
//    {
//        if (playerCamera != null)
//        {
//            float targetFOV = isSprinting ? originalFOV + runFOVIncrease : originalFOV;
//            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovChangeSpeed);
//        }
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = true;
//        }
//    }

//    void OnCollisionExit(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = false;
//        }
//    }
//}
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 7f;
    public float slideDistance = 15f;
    public float sprintCooldown = 2f;
    public float teleportCooldown = 2f;
    public float maxStamina = 5f;
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float slideZoomAmount = 0.8f;
    public float slideZoomSpeed = 10f;
    public float runFOVIncrease = 10f;
    public float fovChangeSpeed = 5f;
    public float leanAngle = 15f;
    public float leanSpeed = 5f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool isCrouching;
    private bool canTeleport = true;
    private Vector3 originalScale;
    private float stamina;
    private bool isSprinting;
    private float rotationX = 0f;
    private Camera playerCamera;
    private float originalFOV;
    private bool sprintOnCooldown = false;
    private float targetLean = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        originalScale = transform.localScale;
        stamina = maxStamina;
        playerCamera = cameraTransform.GetComponent<Camera>();
        if (playerCamera != null)
        {
            originalFOV = playerCamera.fieldOfView;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        HandleJump();
        HandleCrouch();
        HandleSprint();
        Teleport();
        CameraControl();
        HandleCameraFOV();
        HandleAutoLean();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * moveX + transform.forward * moveZ;
        float currentSpeed = isSprinting ? sprintSpeed : (isCrouching ? crouchSpeed : moveSpeed);

        rb.linearVelocity = new Vector3(moveDir.x * currentSpeed, rb.linearVelocity.y, moveDir.z * currentSpeed);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                canDoubleJump = false;
            }
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            transform.localScale = new Vector3(originalScale.x, originalScale.y / 2, originalScale.z);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            transform.localScale = originalScale;
        }
    }

    void HandleSprint()
    {
        if (!sprintOnCooldown && Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            isSprinting = true;
            stamina -= Time.deltaTime;
            if (stamina <= 0)
            {
                stamina = 0;
                sprintOnCooldown = true;
                Invoke("ResetSprintCooldown", sprintCooldown);
            }
        }
        else
        {
            isSprinting = false;
            if (stamina < maxStamina && !sprintOnCooldown)
                stamina += Time.deltaTime;
        }
    }

    void ResetSprintCooldown()
    {
        sprintOnCooldown = false;
    }

    void Teleport()
    {
        if (Input.GetKeyDown(KeyCode.C) && canTeleport)
        {
            canTeleport = false;
            rb.AddForce(transform.forward * slideDistance, ForceMode.VelocityChange);
            if (playerCamera != null)
            {
                StartCoroutine(SlideCameraEffect());
            }
            Invoke("ResetTeleport", teleportCooldown);
        }
    }

    System.Collections.IEnumerator SlideCameraEffect()
    {
        float elapsedTime = 0f;
        float targetFOV = originalFOV * slideZoomAmount;
        while (elapsedTime < 0.2f)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, elapsedTime * slideZoomSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        elapsedTime = 0f;
        while (elapsedTime < 0.2f)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, originalFOV, elapsedTime * slideZoomSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerCamera.fieldOfView = originalFOV;
    }

    void HandleAutoLean()
    {
        float moveX = Input.GetAxis("Horizontal");
        targetLean = -moveX * leanAngle;
        cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, Quaternion.Euler(rotationX, 0, targetLean), Time.deltaTime * leanSpeed);
    }

    void CameraControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, targetLean);
    }

    void HandleCameraFOV()
    {
        if (playerCamera != null)
        {
            float targetFOV = isSprinting ? originalFOV + runFOVIncrease : originalFOV;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovChangeSpeed);
        }
    }

    void ResetTeleport()
    {
        canTeleport = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
