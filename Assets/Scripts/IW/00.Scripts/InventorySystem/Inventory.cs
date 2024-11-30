using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

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

[System.Serializable]
public class CollectedItemData
{
    public List<string> collectedItemNames; // ItemName 저장
}

public class Inventory : MonoBehaviour
{
    private int _currentStartIndex = 0; // 현재 보여지는 아이템의 시작 인덱스
    private int _maxVisibleSlots = 10; // 최대로 보여질 아이템의 인덱스

    private string _filePath;
    private string _currentButtonState;

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

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.Log("Duplicate Inventory detected");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (_icon == null || _info == null)
        {
            _icon = GameObject.Find("Item Image").GetComponent<Image>();
            _info = GameObject.Find("Item Info").GetComponent<TextMeshProUGUI>();
        }

        LoadInventory();
        LoadCollectedItems();
        GenerateCollectedItems();
    }

    private void Update()
    {
        if (_carriedItem == null) return;
        else if (_collectedItem != null) _carriedItem.transform.position = Input.mousePosition;
    }

    public void SetButtonState(string buttonState)
    {
        _currentButtonState = buttonState;
    }

    public string GetCurrentButtonState()
    {
        return _currentButtonState;
    }

    public void GetKeyCodeA()
    {
        Debug.Log("A");
        if (_collectedItem.Count == 0) return;

        if (_currentStartIndex > 0)
        {
            _currentStartIndex = (_currentStartIndex - 1 + _collectedItem.Count) % _collectedItem.Count;
            UpdateVisibleItems();
            Debug.Log("moved left : currentIndex = " + _currentStartIndex);
        }
    }

    public void GetKeyCodeD()
    {
        Debug.Log("D");
        if (_currentStartIndex < _collectedItem.Count - _maxVisibleSlots)
        {
            _currentStartIndex = (_currentStartIndex + 1) % _collectedItem.Count;
            UpdateVisibleItems();
            Debug.Log("moved right : currentIndex : " + _currentStartIndex);
        }
    }

    private void UpdateVisibleItems()
    {
        // 현재 페이지에 맞는 아이템만 갱신
        for (int i = 0; i < _maxVisibleSlots; i++)
        {
            int itemIndex = _currentStartIndex + i;

            // 이미 생생되어있으면 초기화, 없으면 새로 생성
            InventoryItem existingItem = _inventorySlots[i].GetComponentInChildren<InventoryItem>();

            if (existingItem != null)
            {
                // 기존 프리팹 삭제
                Destroy(existingItem.gameObject);
            }

            if (itemIndex >= 0 && itemIndex < _collectedItem.Count)
            {
                ItemSO itemSO = _collectedItem[itemIndex];

                InventoryItem newItem = Instantiate(_itemPrefabs, _inventorySlots[i].transform);
                newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(165, 180);
                newItem.Initialize(itemSO, _inventorySlots[i]);

                _inventorySlots[i].SetItem(newItem);

                if (i == 0)
                {
                    ChangeIcon(itemSO);
                }
            }
            else
            {
                // 슬롯을 비워주는 코드 추가
                _inventorySlots[i].ClearSlot();
            }
        }
    }

    private void UpdateInventorySlots()
    {
        foreach (var slot in _inventorySlots)
        {
            slot.ClearSlot();
        }

        for (int i = 0; i < _maxVisibleSlots; i++)
        {
            int itemIndex = _currentStartIndex + i;

            if (itemIndex >= 0 && itemIndex < _collectedItem.Count)
            {
                SpawnInventoryItem(_collectedItem[itemIndex]);
            }
        }
    }

    // Save Inventory To Json
    private void SaveInventory()
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

    private void SaveCollectedItems()
    {
        Debug.Log("SaveCollectedItems");
        var collectedItemNames = new List<string>();
        foreach (var item in _collectedItem)
        {
            Debug.Log("add");
            collectedItemNames.Add(item.ItemName);
        }

        var data = new CollectedItemData { collectedItemNames = collectedItemNames };
        string json = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/CollectedItems.json";
        File.WriteAllText(filePath, json);
        Debug.Log(filePath);
        Debug.Log(data + " : Collected Items saved");
    }

    private void LoadInventory()
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
            if (itemSO != null && _collectedItem.Contains(itemSO))
            {
                _collectedItem.Add(itemSO);
                SpawnInventoryItem(itemSO);
            }
            else
            {
                Debug.Log("Item " + slotData.itemName + " is not collected. Skipping spawn");
            }
        }

        GenerateCollectedItems();
        Debug.Log("Inventory data loaded");
    }

    private void LoadCollectedItems()
    {
        string filePath = Application.persistentDataPath + "/CollectedItems.json";  
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var data = JsonUtility.FromJson<CollectedItemData>(json);

            if (data != null && data.collectedItemNames != null)
            {
                _collectedItem.Clear();
                foreach (var itemName in data.collectedItemNames)
                {
                    var item = System.Array.Find(_items, i => i.ItemName == itemName);
                    if (item != null)
                    {
                        _collectedItem.Add(item); // 복구된 ItemSO 추가
                    }
                    else
                    {
                        Debug.LogWarning("Item not found : " + itemName);
                    }
                }
                Debug.Log("Collected Items loaded");
            }
        }
        else
        {
            Debug.Log("No collected items file found. Starting with an empty list.");
            _collectedItem = new List<ItemSO>();
        }
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
        SaveCollectedItems();
    }

    public void OnSceneChange()
    {
        SaveInventory();
        SaveCollectedItems();
        GenerateCollectedItems();
    }

    public void GenerateCollectedItems()
    {
        foreach (var slot in _inventorySlots)
        {
            slot.ClearSlot();
        }

        _currentStartIndex = 0;
        UpdateInventorySlots();

        foreach (var itemSO in _collectedItem)
        {
            bool isAlreadyInInventory = false;

            foreach (var slot in _inventorySlots)
            {
                // 슬롯이 이미 아이템을 가지고 있는가?
                if (slot._myItem != null && slot._myItem.ItemSO == itemSO)
                {
                    isAlreadyInInventory = true;
                    break;
                }
            }

            if (!isAlreadyInInventory)
            {
                foreach (var slot in _inventorySlots)
                {
                    if (slot._myItem = null) // 비어있는 슬롯에만 추가해주기
                    {
                        var newItem = Instantiate(_itemPrefabs, slot.transform);
                        newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(165, 180);

                        newItem.Initialize(itemSO, slot);
                        slot.SetItem(newItem);

                        // 핸들러 연결
                        var suggestEvidence = FindObjectOfType<scrSuggestEvidence>();
                        var selectEvidence = FindObjectOfType<scrSelectEvidence>();
                        if (suggestEvidence != null)
                        {
                            newItem.OnSubmitEvidence.AddListener(suggestEvidence.HandleEvidenceSubmission);
                        }
                        else
                        {
                            Debug.Log("scrSuggestEvidence scr not found in the scene");
                        }
                        if(selectEvidence != null)
                        {
                            newItem.OnSubmitEvidence.AddListener(selectEvidence.HandleEvidenceSubmission);
                        }
                        else
                        {
                            Debug.Log("scrSuggestEvidence scr not found in the scene");
                        }
                        break;
                    }
                }
            }
        }
        Debug.Log("Collected items generated and added to inventory");
    }

    // 아이템 잘 생성되는 지
    public void SpawnInventoryItem(ItemSO item = null)
    {
        ItemSO itemSO = item;
        if (itemSO == null)
        {
            Debug.LogError("SpawnInventoryItem called with null ItemSO");
            //int random = UnityEngine.Random.Range(0, _items.Length);
            //itemSO = _items[random];
            return;
        }

        if (item == null)
        {
            Debug.LogError("SpawnInventoryItem called");
        }

        if (!_collectedItem.Contains(itemSO))
        {
            Debug.Log("Item " + itemSO.ItemName + " is not collected. Skipping spawn");
            return;
        }

        if (_inventorySlots == null || _inventorySlots.Length == 0)
        {
            Debug.LogError("_inventorySlots is not initialized or has no slots.");
            return;
        }

        if (_itemPrefabs == null)
        {
            Debug.LogError("_itemPrefabs is not assigned in the Inspector.");
            return;
        }

        for (int i = 0; i < _inventorySlots.Length;  i++)
        {
            if (_inventorySlots[i] == null)
            {
                Debug.LogError("Inventory slot at index " + i + " is null.");
                return;
            }

            // 슬롯이 비어있는지 체크
            if (_inventorySlots[i]._myItem == null)
            {
                var newItem = Instantiate(_itemPrefabs, _inventorySlots[i].transform);
                newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(165, 180);

                newItem.Initialize(item, _inventorySlots[i]);

                if (scrSuggestEvidence.Instance != null)
                {
                    newItem.OnSubmitEvidence.RemoveAllListeners();
                    newItem.OnSubmitEvidence.AddListener(scrSuggestEvidence.Instance.HandleEvidenceSubmission);
                }
                else
                {
                    Debug.LogWarning("scrSuggestEvidence.Instance is null.");
                }

                if (scrSelectEvidence.Instance != null)
                {
                    newItem.OnSuggestEvidence.RemoveAllListeners();
                    newItem.OnSuggestEvidence.AddListener(scrSelectEvidence.Instance.HandleEvidenceSubmission);
                }
                else
                {
                    Debug.LogWarning("scrSelectEvidence.Instance is null.");
                }

                //newItem.OnSubmitEvidence.RemoveAllListeners();
                //newItem.OnSubmitEvidence.AddListener(scrSuggestEvidence.Instance.HandleEvidenceSubmission);
                //newItem.OnSuggestEvidence.RemoveAllListeners();
                //newItem.OnSuggestEvidence.AddListener(scrSelectEvidence.Instance.HandleEvidenceSubmission);

                SaveInventory();
                return;
            }
        }

        Debug.LogWarning("No empty slots available for the Item.");
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
        try
        {
            if (itemSO == null)
            {
                Debug.LogError("itemSO is Null");
                return;
            }
            string currentButtonState = Inventory.Instance.GetCurrentButtonState(); // 현재 버튼 상태 가져오기

            if (!_collectedItem.Exists(item => item.ItemName == itemSO.ItemName))
            {
                _collectedItem.Add(itemSO);
                Debug.Log("Item Added : " + itemSO.ItemName);
                SpawnInventoryItem(itemSO);
                SaveCollectedItems();

                if (currentButtonState == "Submit")
                {
                    OnSubmitEvidence(itemSO); // Submit 이벤트 처리
                }
                else if (currentButtonState == "Suggest")
                {
                    OnSuggestEvidence(itemSO); // Suggest 이벤트 처리
                }
                else
                {
                    Debug.Log("No action for the current button state.");
                }
            }
            else
            {
                Debug.Log("Item is already in the list");
                foreach (var item in _collectedItem)
                {
                    Debug.Log("Existing Item : " + item.ItemName);
                }
                Debug.Log("Attempted to Add : " + itemSO.ItemName);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void OnSubmitEvidence(ItemSO item)
    {
        Debug.Log("Submit action triggered for item: " + item.ItemName);
        // Submit 이벤트 처리 로직
    }

    // Suggest 이벤트
    public void OnSuggestEvidence(ItemSO item)
    {
        Debug.Log("Suggest action triggered for item: " + item.ItemName);
        // Suggest 이벤트 처리 로직
    }
}
