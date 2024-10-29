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

    // ������ �� �����Ǵ� ��
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
            // ������ ����ִ��� üũ
            if (_inventorySlots[i]._myItem == null)
            {
                Instantiate(_itemPrefabs, _inventorySlots[i].transform).Initialize(itemSO, _inventorySlots[i]);
                Debug.Log("�����׹� ����");
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
