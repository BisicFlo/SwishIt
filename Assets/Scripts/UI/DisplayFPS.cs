using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFPS : MonoBehaviour {

    [SerializeField] private Text uiText;

    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;

    private float refreshTime = 1;

    void Awake() {
        frameDeltaTimeArray = new float[20];
        StartCoroutine(UpdateText());
    }

    void Update() {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex+1 )%frameDeltaTimeArray.Length;

       
    }

    private float CalculateFPS() {
        float total = 0f;

        for (int i = 0; i < frameDeltaTimeArray.Length; i++) {
            total += frameDeltaTimeArray[i];  
        }
        return frameDeltaTimeArray.Length/total;
    }

    IEnumerator  UpdateText() {
        WaitForSeconds waitTime = new WaitForSeconds(refreshTime);

        while (true) {
            yield return waitTime;
            uiText.text = Mathf.RoundToInt(CalculateFPS()).ToString();
        }
    }
}
