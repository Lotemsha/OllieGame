using UnityEngine;

[CreateAssetMenu(fileName = "CombatSettings", menuName = "Combat/Combat Settings")]
public class CombatSettings : ScriptableObject
{
    [Header("Critical Fail Chances")]
    public float panicCritFail = 0.10f;
    public float freezeCritFail = 0.05f;
    public float highCritFail = 0.02f;

    [Header("Critical Hit Multipliers")]
    public float basicCritMultiplier = 1.5f;
    public float strongCritMultiplier = 1.75f;

    [Header("Accuracy Limits")]
    public float minAccuracy = 0.15f;
    public float maxAccuracy = 0.95f;

    [Header("Strong Attack")]
    public int strongAttackStunChance = 25;

    [Header("Miss Chances")]
    public int highMissPercent = 10;
    public int panicMissPercent = 25;

    [Header("Lose Turn Chances")]
    public int lowLoseTurnPercent = 3;
    public int freezeLoseTurnPercent = 30;

    [Header("Self Damage")]
    public float selfDamageBase = 5f;
    public float selfDamagePerLevel = 1.5f;

    [Header("Damage Variance")]
    public float varianceMin = 0.85f;
    public float varianceMax = 1.15f;
}
