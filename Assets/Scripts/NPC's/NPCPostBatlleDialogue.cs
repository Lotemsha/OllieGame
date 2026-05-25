using UnityEngine;

public class NPCPostBatlleDialogue : MonoBehaviour
{
    void Start()
    {
        Debug.Log("showPostBattleDialogue: " + GameController.Instance.showPostBattleDialogue);
        Debug.Log("pendingDialogue: " + GameController.Instance.pendingDialogue);

        if (GameController.Instance.showPostBattleDialogue)
        {
            Debug.Log("Showing post-battle dialogue!");

            DialogueUI ui = FindObjectOfType<DialogueUI>();
            ui.ShowDialogue(GameController.Instance.pendingDialogue);

            GameController.Instance.showPostBattleDialogue = false;
            GameController.Instance.pendingDialogue = null;
        }
    }
}
