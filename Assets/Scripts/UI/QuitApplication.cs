using UnityEngine;

// Used for Debug Only

public class QuitApplication : MonoBehaviour {


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            QuitGame();
        }
    }

    public void QuitGame() {

#if UNITY_EDITOR
        // Exits Play Mode in the Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Closes the standalone application
            Application.Quit(); 
#endif
    }
}
