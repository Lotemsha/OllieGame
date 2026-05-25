using UnityEngine;

public class BagButton : MonoBehaviour
{
    public InventoryUI inventoryUI;

    public void OnBagClicked()
    {
        inventoryUI.Open();
    }
}