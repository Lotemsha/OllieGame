using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    private NPCController npc;
    private DialogueUI dialogueUI;

    private bool playerInRange = false;

    void Start()
    {
        npc = GetComponent<NPCController>();
        dialogueUI = FindObjectOfType<DialogueUI>();
    }

    void Update()
    {
        if (!playerInRange) 
            return;

        if (!npc.npcData.isDefeated)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueUI.ShowDialogue(npc.dialogue.lines);
            Debug.Log("Dialogue text: " + npc.CharacterData.DialogueText);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        dialogueUI.HideDialogue();
    }
}
