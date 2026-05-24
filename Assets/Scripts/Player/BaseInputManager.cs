using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseInputManager : MonoBehaviour { // PlayerInput

    public InputActionAsset InputActions;

    protected InputAction InteractionAction;  // Click F  / Left Click
    protected InputAction MoveAction;        // Move ZQSD / WASD / ^<v>
    protected InputAction LookAction;       // Mouse
    protected InputAction ClickAction;     // Unused

    // Could use "InputActionReference" instead of "InputAction" ?

    protected virtual void Awake() {
        InteractionAction = InputActions.FindAction("Interaction");
        MoveAction = InputActions.FindAction("Move");
        LookAction = InputActions.FindAction("Look");
        //ClickAction = InputActions.FindAction("Click");
    }

    protected void OnEnable() {
        InteractionAction.performed += InteractionPerformed;
        InteractionAction.canceled += InteractionCanceled;
        MoveAction.performed += MovePerformed;
        LookAction.performed += LookPerformed;
        //ClickAction.performed += ClickPerformed;
    }

    protected void OnDisable() {
        InteractionAction.performed -= InteractionPerformed;
        InteractionAction.canceled -= InteractionCanceled;
        MoveAction.performed -= MovePerformed;
        LookAction.performed -= LookPerformed;
        //ClickAction.performed -= ClickPerformed;
    }

    protected virtual void InteractionPerformed(InputAction.CallbackContext context) {
        //Debug.Log("InteractionPerformed");
    }
    protected virtual void InteractionCanceled(InputAction.CallbackContext context) {
        //Debug.Log("InteractionCanceled");
    }
    protected void MovePerformed(InputAction.CallbackContext context) {
        //Debug.Log("MovePerformed");
    }
    protected void LookPerformed(InputAction.CallbackContext context) {
       // Debug.Log("LookPerformed");
    }
    protected void ClickPerformed(InputAction.CallbackContext context) {
        //Debug.Log("ClickPerformed");
    }

}
