using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image _itemIcon;
    [SerializeField] private ItemSO _currentItem;
    public CanvasGroup _canvasGroup { get; private set; }

    public ItemSO ItemSO { get; private set; }
    public InventorySlot _activeSlot { get; set; }

    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemText;

    public UnityEvent<ItemSO> OnSubmitEvidence = new UnityEvent<ItemSO>();
    public UnityEvent<ItemSO> OnSuggestEvidence = new UnityEvent<ItemSO>();

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _itemIcon = GetComponent<Image>();
    }

    // 아이템 초기화
    public void Initialize(ItemSO items, InventorySlot parent)
    {
        _activeSlot = parent;
        _activeSlot._myItem = this;
        _currentItem = items;
        ItemSO = items;
        parent.SetItem(this);
        Debug.Log("현재 들어온 아이템 : " + _currentItem.name);

        if (_itemIcon != null && _currentItem.ItemIcon != null)
        {
            _itemIcon.sprite = _currentItem.ItemIcon;
        }
        else
        {
            Debug.LogWarning("ItemIcon 또는 _itemIcon이 null입니다.");
        }
    }

    // 마우스 클릭 이벤트 처리 (아이템 클릭)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Clicked on item : " + _currentItem.ItemName);
            Inventory.Instance.ChangeIcon(_currentItem);

            if (ItemSO != null)
            {
                Inventory.Instance.OnItemClicked(ItemSO);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (ItemSO != null)
            {
                Debug.Log("Right Clicked : " + ItemSO.ItemName);

                OnSubmitEvidence.Invoke(ItemSO); // Submit 이벤트 처리
                OnSuggestEvidence.Invoke(ItemSO); // Suggest 이벤트 처리
            }
            else
            {
                Debug.Log("Item SO is null, cannot submit evidence");
            }
        }
    }
}
