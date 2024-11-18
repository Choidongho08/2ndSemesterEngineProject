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
        // LoadInventory();
    }

    /*SaveInventoryToJson
    public void SaveInventory()
    {
        File.WriteAllText(_filePath, JsonUtility.ToJson(this));
        Debug.Log("Saving Data");
    }

    public Inventory LoadInventory()
    {
        if (File.Exists(_filePath))
        {
            string jsonData = File.ReadAllText(_filePath);
            Debug.Log("Reloading Save Data");
            return JsonUtility.FromJson<Inventory>(jsonData);  
        }
        else
        {
            Debug.LogError("Save File Not Found");
            return null;
        }
    }*/

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
                Debug.Log(itemSO);
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
