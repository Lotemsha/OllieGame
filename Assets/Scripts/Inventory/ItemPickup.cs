using CoreClasses.Models;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var player = GameController.Instance.player;
        var item = ItemDatabase.GetByID(itemID);

        if (item != null)
        {
            string msg = player.Inventory.AddItem(item);
            Debug.Log(msg);
        }

        Destroy(gameObject);
    }
}
