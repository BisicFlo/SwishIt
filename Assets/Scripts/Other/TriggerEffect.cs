using UnityEngine;

public class TriggerEffect : MonoBehaviour {

    [SerializeField] private ParticleSystem effect;

    //void Update() {        
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        effect.Play();
    //    }
    //}


    private void OnTriggerEnter(Collider other) {
        if (effect != null) {
            effect.Play();
        }
    }
}
