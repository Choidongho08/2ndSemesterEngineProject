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
    [SerializeField] private ItemSO _currentItem;
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
        _currentItem = items;
        Debug.Log("현재 들어온 아이템 : " + _currentItem.name);
        _myItem = items;
        _itemIcon.GetComponent<Image>().sprite = _currentItem.ItemIcon; // 여기서 오류 | itemIcon 이 null 값
        Debug.Log(items.sprite);
        Debug.Log("--------------");
        Debug.Log(_itemIcon.sprite);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Instance.SetCarriedItem(this);
        }
    }
}
