using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private string[] currentLines;
    private int index = 0;
    private bool isActive = false;

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string text)
    {
        if (dialoguePanel == null || dialogueText == null)
            return;

        dialoguePanel.SetActive(true);
        dialogueText.text = text;
        isActive = true;
    }
    public void ShowDialogue(string[] lines)
    {
        if (dialoguePanel == null || dialogueText == null)
            return;

        if (lines == null || lines.Length == 0)
            return;

        currentLines = lines;
        index = 0;

        dialoguePanel.SetActive(true);
        dialogueText.text = currentLines[0];
        isActive = true;
    }
    public void HideDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        isActive = false;
    }
    void Update()
    {
        if (!isActive)
            return;

        if (dialoguePanel == null || dialogueText == null)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            index++;

            if (currentLines == null || index >= currentLines.Length)
            {
                HideDialogue();
                return;
            }

            dialogueText.text = currentLines[index];
        }
    }
}