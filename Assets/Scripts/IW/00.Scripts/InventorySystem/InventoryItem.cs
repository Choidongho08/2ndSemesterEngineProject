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

    public ItemSO ItemSO => _currentItem;
    public InventorySlot _activeSlot { get; set; }

    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemText;

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
        Debug.Log("���� ���� ������ : " + _currentItem.name);

        if(_itemIcon != null && _currentItem.ItemIcon != null)
        {
            _itemIcon.sprite = _currentItem.ItemIcon;
        }
        else
        {
            Debug.LogWarning("ItemIcon �Ǵ� _itemIcon�� null�Դϴ�.");
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Clicked on item : " + _currentItem.name);
            Inventory.Instance.ChangeIcon(_currentItem);

            // Inventory.Instance.SetCarriedItem(this);
        }
        else
        {
            Debug.LogWarning("No current item assigned to this InventoryItem");
        }
    }
}
