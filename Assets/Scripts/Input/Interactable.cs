using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used for Buttons / levers / ...
/// </summary>
public class Interactable : MonoBehaviour {
    [Header("Interaction Settings")]
    [Tooltip("Functions called when this object is interacted with")]
    public UnityEvent onInteract = new UnityEvent();

    public void Interact() {
        Debug.Log("Interact");
        onInteract?.Invoke();
    }

    // Optional
    [ContextMenu("Trigger Interaction")]
    private void TriggerInteractionDebug() {
        Interact();
    }
}
