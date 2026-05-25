using UnityEngine;

[CreateAssetMenu(fileName = "BallData", menuName = "Scriptable Objects/BallData")]
public class BallData : ScriptableObject {

    public string BallName ;

    public GameObject BallPrefab = null;

    public int Price;                   // Price in the shop
    public float Scale = 1;             // Affects the scale of the ball // Unused
    public float SpeedMultiplier = 1;   // Affects the speed of the ball
    public int Money = 1;               //Amount of "Money" when scoring
    public int Xp = 1;                  //Amount of "Xp" when scoring

}
