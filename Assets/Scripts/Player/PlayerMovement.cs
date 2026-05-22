using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public InputActionAsset InputActions;

    [Header("Movement")]
    public Rigidbody rb;
    public float moveSpeed;
    


    [Header("Look")]
    public Transform cameraTransform; // Drag your Camera here
    public float lookSensitivity = 0.1f;
    private float verticalRotation = 0f;


    private Vector2 moveDirection;
    private Vector2 lookDirection;


    private InputAction InteractionAction; // Click F
    private InputAction MoveAction;       // Move ZQSD / WASD / ^<v>
    private InputAction LookAction;
    private InputAction ClickAction;

    // Could use "InputActionReference" instead of "InputAction" ?

    private void Awake() {
        InteractionAction = InputActions.FindAction("Interaction");
        MoveAction = InputActions.FindAction("Move");
        LookAction = InputActions.FindAction("Look");
        ClickAction = InputActions.FindAction("Click");

        // Lock and hide cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void OnEnable() {
        InteractionAction.performed += InteractionPerformed;
        InteractionAction.canceled += InteractionCanceled;
        MoveAction.performed += MovePerformed;
        LookAction.performed += LookPerformed;
        ClickAction.performed += ClickPerformed;

    }

    private void OnDisable() {
        InteractionAction.performed -= InteractionPerformed;
        InteractionAction.canceled -= InteractionCanceled;
        MoveAction.performed -= MovePerformed;
        LookAction.performed -= LookPerformed;
        ClickAction.performed -= ClickPerformed;

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


    private void InteractionPerformed(InputAction.CallbackContext context) {
        Debug.Log("InteractionPerformed");
    }
    private void InteractionCanceled(InputAction.CallbackContext context) {
        Debug.Log("InteractionCanceled");
    }
    private void MovePerformed(InputAction.CallbackContext context) {
        Debug.Log("MovePerformed");
    }
    private void LookPerformed(InputAction.CallbackContext context) {
        Debug.Log("LookPerformed");
    }
    private void ClickPerformed(InputAction.CallbackContext context) {
        Debug.Log("ClickPerformed");
    }

}
