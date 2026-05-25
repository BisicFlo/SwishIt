using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour {
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Text textDisplay;

    private void OnEnable() {
        playerData.OnAnyStatChanged.AddListener(UpdateUI); // myButton.onClick.AddListener(() => MyFunction(myVariable));

        UpdateUI(); // Initial 
    }

    private void OnDisable() {
        playerData.OnAnyStatChanged.RemoveListener(UpdateUI); 
    }

    private void UpdateUI() {
        textDisplay.text = playerData.Money.ToString();
    }

}
