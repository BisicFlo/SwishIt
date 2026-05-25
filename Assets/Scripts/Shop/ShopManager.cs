using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public static ShopManager Instance { get; private set; } // Singleton // Not Used 

    [SerializeField] private PlayerData playerData; // for money access

    [SerializeField] private List<ShopElement> shopElements = new List<ShopElement>();

    [SerializeField] private List<BallData> ballDataList = new List<BallData>();


    private void Awake() {
        if (Instance != null) Debug.LogWarning("More than one ShopManager detected");
        Instance = this;

        SetupShop();
    }

    private void OnEnable() {
        SetupButtonsEvents();
    }
    private void OnDisable() {
        RemoveButtonsEvents();
    }

    private void SetupButtonsEvents() {
        for (int i = 0; i < shopElements.Count; i++) {
            int buttonIndex = i; // used to save the index in the lambda expression  -> OnClick(i) stores OnClick(4)
            shopElements[i].Interactable.onInteract.AddListener(() => OnClick(buttonIndex));
        }
    }
    private void RemoveButtonsEvents() {
        for (int i = 0; i < shopElements.Count; i++) {
            shopElements[i].Interactable.onInteract.RemoveAllListeners();
        }
    }
    public void OnClick(int index) {
        Debug.Log("OnClick");
        BallData ball = ballDataList[index];

        if (playerData.BuySomething(ball.Price)) { // If Player can buy -> buy 

            playerData.ChangeBall(ball);

        }

    }
    private void SetupShop() {
        for (int i = 0; i < shopElements.Count; i++) {

            BallData ball = ballDataList[i];

            // 3D Models
            GameObject prefab = ball.BallPrefab;
            Transform transform = shopElements[i].SpawnPoint;
            GameObject go = Instantiate(prefab, transform.position, transform.rotation);

            // Prices
            shopElements[i].PriceText.text = ball.Price.ToString();
        }
    }
}
