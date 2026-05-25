using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : BaseInputManager { // PlayerInput  

    [Header("Movement")]
    public Rigidbody rb;
    public float moveSpeed; 

    [Header("Look")]
    public Transform cameraTransform; // Drag your Camera here
    public float lookSensitivity = 0.1f;
    private float verticalRotation = 0f;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    protected override void Awake() {
        base.Awake();

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    } 

    private void Update() {
        moveDirection = MoveAction.ReadValue<Vector2>();
        lookDirection = LookAction.ReadValue<Vector2>();

        // Vertical look -> camera only
        verticalRotation -= lookDirection.y * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Horizontal look -> rotate the whole player
        transform.Rotate(Vector3.up * lookDirection.x * lookSensitivity);
    }


    private void FixedUpdate() {
        // Need to be optimised
        Vector3 move = (transform.right * moveDirection.x + transform.forward * moveDirection.y) * moveSpeed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }
}
