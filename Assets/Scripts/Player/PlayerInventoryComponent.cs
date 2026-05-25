using CoreClasses.Models;
using UnityEngine;

public class PlayerInventoryComponent : MonoBehaviour
{
    public PlayerManager Player => GameController.Instance.player;
    public InventoryManager Inventory => GameController.Instance.player.Inventory;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
