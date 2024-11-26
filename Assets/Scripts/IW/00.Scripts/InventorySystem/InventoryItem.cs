using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.Events;

public class ItemEvent : UnityEvent<ItemSO> { }

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image _itemIcon;
    [SerializeField] private ItemSO _currentItem;
    public CanvasGroup _canvasGroup { get; private set; }

    public ItemSO ItemSO { get; private set; }
    public InventorySlot _activeSlot { get; set; }

    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemText;

    [Header("Item Events")]
    public ItemEvent OnItemSelectEvent = new ItemEvent(); // ���� ���ÿ��� ������ ������ SO �̺�Ʈ

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _itemIcon = GetComponent<Image>();

        // �̺�Ʈ�� �ʱ�ȭ ���ֱ�
        if (OnItemSelectEvent == null)
        {
            OnItemSelectEvent = new ItemEvent();
        }
    }

    public void Initialize(ItemSO items, InventorySlot parent)
    {
        _activeSlot = parent;
        _activeSlot._myItem = this;
        _currentItem = items;
        ItemSO = items;
        parent.SetItem(this);
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

            if (ItemSO != null)
            {
                Inventory.Instance.OnItemClicked(ItemSO);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnItemSelectEvent != null)
            {
                Debug.Log("Right Click Event : " + _currentItem.ItemName + ", invoking event.......");

                // �̺�Ʈ ȣ�� + ItemSO ������
                OnItemSelectEvent?.Invoke(_currentItem);
            }
            else
            {
                Debug.LogWarning("OnItemSelected is null");
            }
        }
        else
        {
            Debug.LogWarning("No current item assigned to this InventoryItem");
        }
    }
}
