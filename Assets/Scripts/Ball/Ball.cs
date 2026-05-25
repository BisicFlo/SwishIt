using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class Ball : MonoBehaviour {

    [SerializeField] private BallData ballData; // used to init values 

    [HideInInspector] public PlayerData Thrower; // Reference to the player who throw this ball / Set by "Grab"
    [HideInInspector] public UnityEvent OnThrown;
    [HideInInspector] public UnityEvent OnHit;
    [HideInInspector] public float speedMultiplier = 1; // Affects the speed of the ball

    private int Money = 1; //Amount of "Money" when scoring
    private int Xp = 1; //Amount of "Xp" when scoring

    // Audio
    [SerializeField] private AudioSource audioSource;
    private AudioClip collisionSound;
    private AudioClip explodeSound;
    private float velocityThreshold = 2f; // Avoid tiny bumps


    private void OnEnable() {
        this.speedMultiplier = ballData.SpeedMultiplier;
        this.Money = ballData.Money;
        this.Xp = ballData.Xp;

        this.collisionSound = ballData.CollisionSound;
        this.explodeSound = ballData.ExplodeSound;

        this.velocityThreshold = ballData.VelocityThreshold;
    }


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Target")) {

            if (Thrower != null) {
                Thrower.GainMoney(Money);
                Thrower.GainXp(Xp);
            }
            if (explodeSound != null) {
                audioSource.PlayOneShot(explodeSound, 2);

                //Optional: randomize pitch for variety
                audioSource.pitch = Random.Range(1.1f, 1.4f);
            }
        }
    }




    private void OnCollisionEnter(Collision collision) {
        if (collisionSound != null) {
            float speed = collision.relativeVelocity.magnitude;

            if (speed > velocityThreshold) {
                // Optional: scale volume by impact strength
                float volume = Mathf.Clamp01(speed / 20f);
                audioSource.PlayOneShot(collisionSound, volume);

                // Optional: randomize pitch for variety
                audioSource.pitch = Random.Range(0.9f, 1.1f);
            }
        }
        OnHit?.Invoke();
    }
}
