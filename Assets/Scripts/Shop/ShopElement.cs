using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopElement : MonoBehaviour{
    public Transform SpawnPoint;

    public Interactable Interactable;

    public Text PriceText;

    public ShopElement(Transform spawnPoint, Interactable interactable, Text priceText) {
        SpawnPoint = spawnPoint;
        Interactable = interactable;
        PriceText = priceText;       
    }
}
