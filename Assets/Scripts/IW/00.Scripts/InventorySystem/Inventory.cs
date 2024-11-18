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

    public InventorySlot[] _inventorySlots;
    public Transform _draggablesTransform;
    public InventoryItem _itemPrefabs;

    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _info;

    [Header("Item List")]
    public ItemSO[] _items;

    [Header("Collected Items List")]
    public List<ItemSO> _collectedItem = new List<ItemSO>();

    private void OnEnable()
    {
        if (_icon == null || _info == null)
        {
            _icon = GameObject.Find("IconImage").GetComponent<Image>();
            _info = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
        }
    }

    private void Awake()
    {
        Instance = this;
        _fItem = GetComponent<FieldItem>();

        _filePath = Application.persistentDataPath + "/Inventory.json";

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (_collectedItem == null)
            _collectedItem = new List<ItemSO>();

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (_icon == null || _info == null)
        {
            _icon = GameObject.Find("Item Image").GetComponent<Image>();
            _info = GameObject.Find("Item Info").GetComponent<TextMeshProUGUI>();
        }

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
            slot.ClearSlot();
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
                var newItem = Instantiate(_itemPrefabs, _inventorySlots[i].transform);
                newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(165, 180);

                newItem.Initialize(item, _inventorySlots[i]);

                _icon.sprite = item.ItemIcon;
                _info.text = item.ItemInfo;

                SaveInventory();
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

        if (item != null)
        {
            _icon.sprite = item.ItemIcon;
            _info.text = item.ItemInfo;
        }
        else
        {
            Debug.Log("Item is null");
            _icon.sprite = null;
            _info.text = "";
        }
    }

    public void OnItemClicked(ItemSO itemSO)
    {
        if (itemSO == null)
        {
            Debug.LogError("itemSO is Null");
            return;
        }
        if (!_collectedItem.Exists(item => item.ItemName == itemSO.ItemName))
        {
            _collectedItem.Add(itemSO);
            Debug.Log("Item Added : " + itemSO.ItemName);
        }
        else
        {
            Debug.Log("Item is already in the list");
        }
    }
}
