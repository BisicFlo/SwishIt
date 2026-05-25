using UnityEngine;

// Used for Debug Only

public class CapFPS : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) {
            CapAt30();
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            CapAt60();
        }
        if (Input.GetKeyDown(KeyCode.F3)) {
            CapAt90();
        }
    }

    public void CapAt30() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }
    public void CapAt60() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    public void CapAt90() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 90;
    }
    public void CapAtN(int n) {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = n;
    }
    public void UnCap() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
    }
}
