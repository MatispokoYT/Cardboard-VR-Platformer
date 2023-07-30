using UnityEngine;

public class scripts : MonoBehaviour
{
    public Camera playerCamera; // Assign the player camera in the Inspector
    public float moveSpeed = 5f;
    public float jumpForce = 10f; // The force applied when jumping
    public float groundDistance = 0.2f; // The distance from the center of the player to the ground
    public LayerMask groundLayer; // The layer(s) representing the ground

    private bool isGrounded;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for controller input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the camera's forward direction without the vertical component
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Move the player based on the input relative to the camera's forward direction
        MovePlayer(horizontalInput, verticalInput, cameraForward);

        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);

        // Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void MovePlayer(float horizontal, float vertical, Vector3 direction)
    {
        // Calculate the movement vector
        Vector3 movement = (direction * vertical) + (playerCamera.transform.right * horizontal);

        // Normalize the movement vector to prevent diagonal movement being faster
        movement.Normalize();

        // Move the player in the desired direction
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        // Apply upward force to the rigidbody to make the player jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}