using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class InvenData
{
    public List<ItemSlotData> slots = new List<ItemSlotData>();
}

[System.Serializable]
public class ItemSlotData
{
    public string itemName;
}

public class Inventory : MonoBehaviour
{
    private string _filePath;

    public static Inventory Instance;
    public static InventoryItem _carriedItem;

    private FieldItem _fItem;

    [SerializeField] InventorySlot[] _inventorySlots;
    [SerializeField] Transform _draggablesTransform;
    [SerializeField] InventoryItem _itemPrefabs;

    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _info;

    [Header("Item List")]
    public ItemSO[] _items;

    private void Awake()
    {
        Instance = this;
        _fItem = GetComponent<FieldItem>();

        _filePath = Application.persistentDataPath + "/Inventory.json";
    }

    private void Start()
    {
        LoadInventory();
    }

    // Save Inventory To Json
    public void SaveInventory()
    {
        InvenData data = new InvenData();

        foreach (var slot in _inventorySlots)
        {
            if (slot._myItem != null)
            {
                data.slots.Add(new ItemSlotData
                {
                    itemName = slot._myItem.ItemSO.ItemName
                });
            }
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_filePath, json);
        Debug.Log("Saving Data");
    }

    public void LoadInventory()
    {
        if (!File.Exists(_filePath))
        {
            Debug.LogWarning("No Inventory cave file found");
            return;
        }

        string json = File.ReadAllText(_filePath);
        InvenData data = JsonUtility.FromJson<InvenData>(json);

        foreach (var slot in _inventorySlots)
        {
            // slot.ClearSlot();
        }

        foreach (var slotData in data.slots)
        {
            var itemSO = System.Array.Find(_items, item => item.ItemName == slotData.itemName);
            if (itemSO != null)
            {
                SpawnInventoryItem(itemSO);
            }
        }

        Debug.Log("No Inventory data loaded");
    }

    // 아이템 잘 생성되는 지
    public void SpawnInventoryItem(ItemSO item = null)
    {
        ItemSO itemSO = item;
        if (itemSO == null)
        {
            int random = Random.Range(25, _items.Length - 10);
            itemSO = _items[random];
        }

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            // 슬롯이 비어있는지 체크
            if (_inventorySlots[i]._myItem == null)
            {
                Instantiate(_itemPrefabs, _inventorySlots[i].transform).Initialize(itemSO, _inventorySlots[i]);
                _itemPrefabs.GetComponent<RectTransform>().localScale = _inventorySlots[i].transform.localScale;
                _icon.sprite = item.ItemIcon;
                _info.text = item.ItemInfo;

                // SaveInventory();
                ChangeIcon(itemSO);
                return;
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

    public void ChangeIcon(ItemSO item)
    {
        if (item == null || item.ItemIcon == null || item.ItemInfo == null)
        {
            Debug.LogError("Invalid item data passed to ChangeIcon.");
            return;
        }

        if (_icon == null || _info == null)
        {
            Debug.LogError("_icon or _info is not assigned in the Inspector.");
            return;
        }

        _icon.sprite = item.ItemIcon;
        _info.text = item.ItemInfo;
        Debug.Log("Icon and Info updated for item: " + item.ItemName);
    }
}
