using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image _itemIcon;
    public CanvasGroup _canvasGroup { get; private set; }

    public ItemSO _myItem { get; set; }
    public InventorySlot _activeSlot { get; set; }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _itemIcon = GetComponent<Image>();
    }

    public void Initialize(ItemSO items, InventorySlot parent)
    {
        _activeSlot = parent;
        _activeSlot._myItem = this;
        _myItem = items;
        _itemIcon.sprite = items.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Instance.SetCarriedItem(this);
        }
    }
}
