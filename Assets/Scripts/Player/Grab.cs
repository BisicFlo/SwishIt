using UnityEngine;
using UnityEngine.InputSystem;

public class Grab : BaseInputManager {
    public GameObject player;
    public Transform holdPos;
    public Transform ViewDirection;

    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    private GameObject heldObj; //object 
    private Rigidbody heldObjRb; //rigidbody of object we pick up


    protected override void InteractionPerformed(InputAction.CallbackContext context) {
        //base.InteractionPerformed(context);
        PerformRaycast();
    }

    protected override void InteractionCanceled(InputAction.CallbackContext context) {
        //base.InteractionPerformed(context);
    }

    private void PerformRaycast() {

        if (heldObj == null) {
            RaycastHit hit;
            if (Physics.Raycast(ViewDirection.position, ViewDirection.TransformDirection(Vector3.forward), out hit, pickUpRange)) {

                if (hit.transform.gameObject.CompareTag("Grabbable")) {
                    PickUpObject(hit.transform.gameObject);
                }
            }
        }
        else {
            //DropObject();
            ThrowObject();
        }
    }


    void PickUpObject(GameObject pickUpObj) {
        Debug.Log("Grab : " + pickUpObj.name);
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            heldObj.transform.localPosition = Vector3.zero;
            //make sure object doesnt collide with player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject() {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }
    void ThrowObject() {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(ViewDirection.forward * throwForce);
        heldObj = null;
    }

}
