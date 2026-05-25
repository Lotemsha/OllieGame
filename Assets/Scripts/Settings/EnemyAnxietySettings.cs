using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAnxietySettings", menuName = "Combat/Enemy Anxiety Settings")]
public class EnemyAnxietySettings : ScriptableObject
{
    [Header("Anxiety Increases")]
    public float fearBaseIncrease = 12f;
    public float stressBaseIncrease = 10f;
    public float anxietyBaseIncrease = 15f;
    public float intimidationBaseIncrease = 12f;
    public float impatientBaseIncrease = 18f;

    [Header("Anxiety Decreases")]
    public float shameBaseDecrease = 12f;
    public float hopelessnessBaseDecrease = 16f;
    public float dissociateBaseDecrease = 18f;
    public float numbnessBaseDecrease = 12f;
    public float traumaBaseValue = 18f;
}
