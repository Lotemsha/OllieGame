using CoreClasses.Models;
using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID;
    public KeyCode pickupKey = KeyCode.Space;

    private bool playerInRange = false;
    private PlayerManager player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        player = GameController.Instance.player;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        player = null;
    }

    private void Update()
    {
        if (!playerInRange || player == null) return;

        if (Input.GetKeyDown(pickupKey))
        {
            var item = ItemDatabase.GetByID(itemID);
            if (item != null)
            {
                string msg = player.Inventory.AddItem(item);
                Debug.Log(msg);
            }
            Destroy(gameObject);
        }
    }
}