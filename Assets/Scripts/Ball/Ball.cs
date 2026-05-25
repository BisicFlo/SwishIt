using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour {

    [SerializeField] private BallData ballData; // used to init values 

    [HideInInspector] public PlayerData Thrower; // Reference to the player who throw this ball / Set by "Grab"

    [HideInInspector] public UnityEvent OnThrown ;

    [HideInInspector] public UnityEvent OnHit;

    [HideInInspector] public float speedMultiplier = 1; // Affects the speed of the ball

    private int Money = 1; //Amount of "Money" when scoring

    private int Xp = 1; //Amount of "Xp" when scoring


    private void OnEnable() {
        this.speedMultiplier = ballData.SpeedMultiplier;
        this.Money = ballData.Money;
        this.Xp = ballData.Xp;
    }


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Target")) {

            if (Thrower != null) {
                Thrower.GainMoney(Money);
                Thrower.GainXp(Xp);
            }
        }
    }
    private void OnCollisionEnter(Collision collision) {
        OnHit?.Invoke();
    }


}
