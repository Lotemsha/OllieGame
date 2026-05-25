using CoreClasses.Models;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlot;
    public Transform itemGrid;
    public Button closeButton;

    private PlayerManager _player;

    void Start()
    {
        closeButton.onClick.AddListener(Close);
    }

    public void Open()
    {
        _player = GameController.Instance.player;
        gameObject.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    void Refresh()
    {
        // מוחק slots ישנים
        foreach (Transform child in itemGrid)
            Destroy(child.gameObject);

        // יוצר slot חדש לכל פריט
        foreach (var item in _player.Inventory.ItemsList)
        {
            var slot = Instantiate(itemSlot, itemGrid);
            slot.GetComponent<ItemSlotUI>().Setup(item, OnUseItem);
        }
    }

    void OnUseItem(Item item)
    {
        string result = _player.Inventory.UseItem(item.ItemID, _player);
        Debug.Log(result);
        Refresh();
    }
}