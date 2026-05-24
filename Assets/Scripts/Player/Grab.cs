using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grab : BaseInputManager {

    [SerializeField] private PlayerData Player; // Reference to the player ScriptableObject

    [SerializeField] private Collider playerCollider;
    [SerializeField] private Transform holdPos;
    [SerializeField] private Transform ViewDirection;
    [SerializeField] private Transform ThrowDirection;

    //[SerializeField] private float throwForce = 500f;    //force at which the object is thrown at
    [SerializeField] private float pickUpRange = 5f;     //how far the player can pickup the object from
    [SerializeField] private GameObject heldObj;        //object 
    [SerializeField] private Rigidbody heldObjRb;      //rigidbody of object we pick up
    [SerializeField] private Vector3 heldObjScale;        //DefaultScale 


    [Header("Throw")]
    [SerializeField] private float minThrowForce = 5f;
    [SerializeField] private float maxThrowForce = 800f;
    [SerializeField] private float maxChargeTime = 2f;

    private float chargeStartTime;
    private bool isCharging = false;


    private float ballSpeedMultiplier;

    private void Update() {

        if (isCharging) {
            if (heldObj == null) return;
            float chargeRatio = Mathf.Clamp01((Time.time - chargeStartTime) / maxChargeTime);
            // Scale the ball as feedback
            heldObj.transform.localScale = heldObjScale * (1f + chargeRatio * 0.5f);
        }
    }

    protected override void InteractionPerformed(InputAction.CallbackContext context) {
        //base.InteractionPerformed(context);
        PerformRaycast();
    }

    protected override void InteractionCanceled(InputAction.CallbackContext context) {
        //base.InteractionPerformed(context);
        if (isCharging) PerformThrow();
    }

    private void PerformRaycast() {

        if (heldObj == null) {
            if (Physics.Raycast(ViewDirection.position, ViewDirection.TransformDirection(Vector3.forward), out RaycastHit hit, pickUpRange)) {

                GameObject go = hit.transform.gameObject;

                if (go.CompareTag("Grabbable")) {
                    PickUpObject(go);
                    BallSetup(go);
                }
            }
        }
        else {
            // ThrowObject();
            isCharging = true;
            chargeStartTime = Time.time;
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
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), playerCollider, true);

            heldObjScale = pickUpObj.transform.localScale;
        }
    }

    private void PerformThrow() {
        float chargeTime = Time.time - chargeStartTime;
        float chargeRatio = Mathf.Clamp01(chargeTime / maxChargeTime);
        float throwForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargeRatio);

        ThrowObject(throwForce);
        isCharging = false;

    }

    void ThrowObject(float throwForce) {
        heldObj.transform.localScale = heldObjScale;

        //Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), playerCollider, false); // temp
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(ThrowDirection.forward * throwForce * ballSpeedMultiplier);
        heldObj = null;
    }


    private void BallSetup(GameObject ballObject) {

        Ball ball = ballObject.GetComponent<Ball>();

        if (ball == null) return;

        ball.Thrower = Player;

        ballSpeedMultiplier = ball.speedMultiplier;

        
    }

}
