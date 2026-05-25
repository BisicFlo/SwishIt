using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject {

    public BallData DefaultBall; // default Ball

    public BallData SelectedBall; // type of ball the player will be using / can be changed

    [SerializeField] private int StartingHealth;
    [SerializeField] private int StartingMoney;
    [SerializeField] private int StartingXp;
    [SerializeField][Range(1, 10)] private int StartingLevel = 1;

    public int Health { get; private set; } // Not Used 
    public int Money { get; private set; }
    public int Xp { get; private set; } // Not Used 
    public int Level { get; private set; } = 1; // Not Used 

    public readonly int[] xpRequired = { 2, 2, 6, 10, 20, 36, 60, 68, 80 }; // Not Used 

    // Events
    [HideInInspector] public UnityEvent OnAnyStatChanged = new UnityEvent();

    [HideInInspector] public UnityEvent OnThrow = new UnityEvent();

    [HideInInspector] public UnityEvent OnBallChanged = new UnityEvent();


    private void OnEnable() {
        //Debug.Log("OnEnablePLayerData");
        SelectedBall = DefaultBall;
        Health = StartingHealth;
        Money = StartingMoney;
        Xp = StartingXp;
        Level = StartingLevel;
    }

    public void GainXp(int xpGained) {
        if (Level >= 10) return; //Niveau Max

        Xp += xpGained;
        if (Xp >= xpRequired[Level - 1]) {
            Xp = 0;
            Level++;
        }
        OnAnyStatChanged?.Invoke();
    }
    public void GainMoney(int money) {
        Money += money;
        OnAnyStatChanged?.Invoke();
    }
    public void TakeDamage(int amount) {
        Health -= amount;
        if (Health <= 0) {
            //defeat            
        }
        OnAnyStatChanged?.Invoke();
    }
    public bool BuySomething(int price) {
        if (Money < price) {
            return false; // Not enough money
        }
        else {
            Money -= price;
            OnAnyStatChanged?.Invoke();
            return true;
        }
    }
    public void Throw() {
        OnThrow?.Invoke();
    }
    public void ChangeBall(BallData ballData) {
        SelectedBall = ballData;
        OnBallChanged?.Invoke();
    }
}
