using System.Collections;
using UnityEngine;

/// <summary>
/// Spawn Balls using Pooling // Change Ball Spawned 
/// </summary>
public class BallSpawner : MonoBehaviour {

    [SerializeField] PlayerData playerData;
    [SerializeField] private BallData defaultBall = null;

    private int ballIndex = 0;
    private Rigidbody[] ballArray; // could be a Ball[] ballArray / no need for GetComponent
    private WaitForSeconds waitTime = new WaitForSeconds(1); // timeBeforeSpawn

    private void Awake() {
        ChangeBall(); // Game Start -> We Spawn a Ball
    }

    private void OnEnable() {
        playerData.OnThrow.AddListener(() => StartCoroutine(SpawnAfterSomeTme()));
        playerData.OnBallChanged.AddListener(() => ChangeBall());
    }

    private void OnDisable() {
        playerData.OnThrow.RemoveListener(() => StartCoroutine(SpawnAfterSomeTme()));
        playerData.OnBallChanged.RemoveListener(() => ChangeBall());
    }

    private void ChangeBall() {
        if (playerData.SelectedBall != null) {

            GameObject ballPrefab = playerData.SelectedBall.BallPrefab;

            ClearBallArray();

            // We get player's selected ball for the Spawner Or the default one
            if (ballPrefab != null) ballArray = CreateStockOf<Rigidbody>(ballPrefab, 5);
        }
        else ballArray = CreateStockOf<Rigidbody>(defaultBall.BallPrefab, 5);

        StartCoroutine(SpawnAfterSomeTme());
    }

    private void ClearBallArray() {
        if (ballArray == null) return;
        foreach (Rigidbody ball in ballArray) {
            Destroy(ball.gameObject);
        }
    }

    public void SpawnABall() {
        if (ballArray == null) return;   // NEw
        Rigidbody myBall = GetObjectFromIndex<Rigidbody>(ballArray, ballIndex);
        ballIndex++;
        ballIndex %= ballArray.Length; // Redundant ?

        InstantiateAlternative(myBall.gameObject, this.transform.position, this.transform.rotation, Vector3.one , null);

        myBall.isKinematic = true;
    }

    private IEnumerator SpawnAfterSomeTme() {
        yield return waitTime;
        SpawnABall();
    }

    #region --------- Pooling ---------
    protected void InstantiateAlternative(GameObject gameObject, Vector3 position, Quaternion rotation, Vector3 scale, Transform parent) {
        gameObject.SetActive(true);
        gameObject.transform.SetPositionAndRotation(position, rotation);
        if (parent != null) gameObject.transform.SetParent(parent, true);
        gameObject.transform.localScale = scale;
    }

    protected void DestroyAlternative(GameObject gameObject) {
        gameObject.SetActive(false);
    }

    protected T[] CreateStockOf<T>(GameObject prefab, int count) where T : Object {
        T[] instances = new T[count];

        for (int i = 0; i < count; i++) {
            GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            go.SetActive(false);

            instances[i] = typeof(T) == typeof(GameObject)
                ? (T)(object)go // Double Cast Trick
                : go.GetComponent<T>();
        }
        return instances;
    }

    protected T GetObjectFromIndex<T>(T[] array, int index) where T : Object {
        return array[index];
    }
    #endregion

}
