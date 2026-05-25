using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Dialogue File")]
public class DialogueSO : ScriptableObject
{
    public string npcID;
    [TextArea(3, 10)]
    public string[] lines;
}