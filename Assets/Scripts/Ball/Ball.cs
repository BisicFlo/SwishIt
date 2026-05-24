using UnityEngine;

public class Ball : MonoBehaviour {

    public PlayerData Thrower; // Reference to the player who throw this ball / Set by "Grab"

    public float speedMultiplier = 1; // Affects the speed of the ball

    public int Money = 1; //Amount of "Money" when scoring

    public int Xp = 1; //Amount of "Xp" when scoring


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Target")) {

            if (Thrower != null) {
                Thrower.GainMoney(Money);
                Thrower.GainXp(Xp);
            }
        }
    }
}
