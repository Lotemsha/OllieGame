using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string fullSentence;
    public float typingSpeed = 0.05f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TypeSentence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        dialogueText.text = "";
        foreach (char letter in fullSentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
