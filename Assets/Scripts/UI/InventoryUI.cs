using CoreClasses.Models;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlot;
    public Transform itemGrid;
    public Button closeButton;

    private PlayerManager _player;

    void Awake()
    {
        _player = GameController.Instance.player;
        _player.Inventory.OnInventoryChanged += Refresh;
    }
    void OnDestroy()
    {
        if (_player != null)
            _player.Inventory.OnInventoryChanged -= Refresh;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (gameObject.activeSelf)
                Close();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
            Close();
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
        foreach (Transform child in itemGrid)
            Destroy(child.gameObject);

        var grouped = _player.Inventory.ItemsList
            .GroupBy(i => i.ItemID)
            .ToDictionary(g => g.Key, g => (item: g.First(), count: g.Count()));

        // סדר הצגה
        var order = new[]
        {
        ItemType.Equipment,
        ItemType.Consumable,
        ItemType.EmotionalItem,
        ItemType.QuestItem
    };

        foreach (var type in order)
        {
            var itemsOfType = grouped.Values
                .Where(e => e.item.Type == type)
                .ToList();

            if (itemsOfType.Count == 0) continue;

            // כותרת סקשן
            var header = new GameObject("Header_" + type);
            header.transform.SetParent(itemGrid, false);
            var txt = header.AddComponent<TextMeshProUGUI>();
            txt.text = GetSectionLabel(type);
            txt.fontSize = 18;
            txt.fontStyle = FontStyles.Bold;

            // הפריטים עצמם
            foreach (var entry in itemsOfType)
            {
                var slot = Instantiate(itemSlot, itemGrid);
                slot.GetComponent<ItemSlotUI>().Setup(entry.item, entry.count, OnUseItem);
            }
        }
    }

    void OnUseItem(Item item)
        {
            string result = _player.Inventory.UseItem(item.ItemID, _player);
            Debug.Log(result);
            Refresh();
        }

        string GetSectionLabel(ItemType type) => type switch
        {
            ItemType.Equipment => "🎒Equipment",
            ItemType.Consumable => "🧪Consumable Items",
            //ItemType.EmotionalItem => "💜 פריטים רגשיים",
            ItemType.QuestItem => "📜 Quest Items",
            _ => type.ToString()
        };
}
