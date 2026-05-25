using UnityEngine;

[CreateAssetMenu(fileName = "AnxietySettings", menuName = "Game/Anxiety Settings")]
public class AnxietySettings : ScriptableObject
{
    [Header("Balance Speeds")]
    public float homeBalanceSpeed = 1f;
    public float calmBalanceSpeed = 0.5f;
    public float parkBalanceSpeed = 0.5f;
    public float coffeeShopBalanceSpeed = 0.5f;

    [Header("Anxiety Increase")]
    public float busyStreetIncrease = 1.5f;
    public float superIncrease = 1.5f;
    public float darkAlleyIncrease = 2f;
    public float academicBuildingIncrease = 2.5f;

    [Header("Anxiety Decrease")]
    public float crowdedSeaDecrease = 1f;

    [Header("Healing")]
    public float homeHeal = 3f;
    public float calmHeal = 1f;

    [Header("Speed Modifiers")]
    public float homeSpeedBoost = 1.2f;
    public float parkSpeedBoost = 1.1f;
    public float coffeeShopSpeedBoost = 1.1f;
    public float busyStreetSpeedPenalty = 0.8f;
    public float darkAlleySpeedBoost = 1.3f;

}
