using CoreClasses.Models;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int itemID;
    public void GiveToPlayer()
    {
        var player = GameController.Instance.player;
        var item = ItemDatabase.GetByID(itemID);

        if (item != null)
        {
            string msg = player.Inventory.AddItem(item);
            Debug.Log("NPC gave: " + msg);
            //npc.Inventory.RemoveItem(item);
        }
    }
}
