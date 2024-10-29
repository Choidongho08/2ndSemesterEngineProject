using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public static InventoryItem _carriedItem;

    private FieldItem _fItem;

    [SerializeField] InventorySlot[] _inventorySlots;

    [SerializeField] Transform _draggablesTransform;
    [SerializeField] InventoryItem _itemPrefabs;

    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _Info;

    [Header("Item List")]
    [SerializeField] ItemSO[] _items;

    private void Awake()
    {
        Instance = this;
        _fItem = GetComponent<FieldItem>();
    }

    // 아이템 잘 생성되는 지
    public void SpawnInventoryItem(ItemSO item = null)
    {
        ItemSO itemSO = item;
        if (itemSO == null)
        {
            int random = Random.Range(0, _items.Length);
            itemSO = _items[random];
        }

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            // 슬롯이 비어있는지 체크
            if (_inventorySlots[i]._myItem == null)
            {
                Instantiate(_itemPrefabs, _inventorySlots[i].transform).Initialize(itemSO, _inventorySlots[i]);
                Debug.Log("아이테무 생성");
                //_icon = item.ItemIcon;
                _Info.text = item.ItemInfo;
            }
        }
    }

    private void Update()
    {
        if (_carriedItem == null) return;

        _carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (_carriedItem != null)
            item._activeSlot.SetItem(_carriedItem);

        _carriedItem = item;
        _carriedItem._canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(_draggablesTransform);
    }
}
