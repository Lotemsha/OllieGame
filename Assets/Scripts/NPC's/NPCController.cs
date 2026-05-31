using UnityEngine;
using CoreClasses.Models;
using System;

public class NPCController : MonoBehaviour
{
    public NPC CharacterData { get; private set; }
    public NPCData npcData;
    public DialogueSO dialogue;

    void Awake()
    {
        CharacterData = new NPC(gameObject.name);
        CharacterData.IsFriendly = false;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (npcData != null && npcData.npcSprite != null)
        {
            sr.sprite = npcData.npcSprite;
        }

        if (npcData != null && npcData.isDefeated) 
        {
            CharacterData.DialogueText = npcData.postBattleDialogue.ToString();
            Console.WriteLine(npcData.postBattleDialogue.ToString());
        }
    }
}
