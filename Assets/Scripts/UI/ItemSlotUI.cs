using CoreClasses.Models;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text descriptionText;
    public TMP_Text quantityText;
    public Button useButton;

    public void Setup(Item item, int quantity, Action<Item> onUse)
    {
        itemNameText.text = item.Name;
        descriptionText.text = $"{item.StatName}: +{item.EffectValue}";
        quantityText.text = quantity > 1 ? $"x {quantity}" : "x1";

        bool canUse = item.Type == ItemType.Consumable;
        useButton.gameObject.SetActive(canUse);

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() => onUse(item));
    }
}