using UnityEngine;
using CoreClasses.Models;

public class NPCController : MonoBehaviour
{
    private NPC _npcLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _npcLogic = new NPC("Old Sage");
        _npcLogic.DialogueText = "Welcome to the world, Ollie!";
        _npcLogic.IsFriendly = true;
    }

    // פונקציה שנקראת כשאולי נכנסת לאזור של ה-NPC
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(_npcLogic.Name + " says: " + _npcLogic.DialogueText);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
