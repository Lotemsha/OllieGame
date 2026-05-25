using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Data", menuName = "NPC/Detailed Data")]
public class NPCData : ScriptableObject
{
    [Header("Base Identity")]
    public string npcName;
    public Sprite npcSprite;
    public RuntimeAnimatorController animatorController; // אם יש לו אנימציית ייחודית

    [Header("Dialogue System")]
    [TextArea(3, 5)]
    public string[] postBattleDialogue;

    [Header("Battle Settings")]
    public string customBattleScene = "GenericBattleScene";

    [Header("State")]
    [Tooltip("האם אולי כבר ניצחה את ה-NPC הזה?")]
    public bool isDefeated = false;
    public bool canBattleAgainOnLoss = false;

    [Header("Psychological Mechanics")]
    [Tooltip("כמה ה-NPC משנה את מד החרדה של אולי ברגע שמתחילים לדבר איתו")]
    public float anxietyChangeOnInteract = 0f;
    public bool triggersPanicAttack = false;

    [Header("Rewards & Progression")]
    public int xpRewardOnResolution = 0; // XP שאולי תקבל
    public bool givesQuest = false;
}