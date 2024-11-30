using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Events;

public class scrSuggestEvidence : MonoBehaviour
{
    public List<EvidenceTextSO> EvidenceSO;

    [SerializeField] private Ease _easyType;
    [SerializeField] private GameObject _inventory;

    private Inventory _scrInventory;
    private InventoryItem _scrInventoryItem;
    private EvidenceTextSO _scrEvidenceTextSO;
    private SelectCriminal _scrSelectCriminal;

    private Vector2 _trInven;

    private bool _isEvidenceCorrect;
    private int _correctEvi;

    public static scrSuggestEvidence Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _trInven = _inventory.GetComponent<RectTransform>().anchoredPosition;
        _scrSelectCriminal = FindObjectOfType<SelectCriminal>();
        _scrEvidenceTextSO = _scrSelectCriminal._objCurrentPanel.GetComponent<scrPutCharSO>()._soChar;
    }

    //private void Start()
    //{
    //    var inventoryItems = FindObjectsOfType<InventoryItem>();
    //    foreach (var item in inventoryItems)
    //    {
    //        item.OnSubmitEvidence.RemoveAllListeners();
    //        item.OnSubmitEvidence.AddListener(HandleEvidenceSubmission);
    //        Debug.Log("Added Listener");
    //    }
    //}

    public void EvidenceSuggest()
    {
        Debug.Log("Select Evidence");

        _inventory.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 5), 1f).SetEase(_easyType);
    }

    public void HandleEvidenceSubmission(ItemSO itemSO)
    {
        Debug.Log("HandleEvidenceSubmission called with item: " + (itemSO != null ? itemSO.ItemName : "null"));

        if (itemSO != null)
        {
            Debug.Log("Submitted Evidence : " + itemSO.ItemName);

            _inventory.GetComponent<RectTransform>().DOAnchorPos(_trInven, 1f).SetEase(_easyType);

            ProcessEvidence(itemSO);
        }
        else
        {
            Debug.LogWarning("No Item selected for evidence submission");
        }
    }

    private void ProcessEvidence(ItemSO itemSO)
    {
        bool thisIsRightEvi = false;
        _correctEvi = 0;

        // 증거 제출 처리 로직 추가
        Debug.Log("Processing Evidence : " + itemSO.ItemName);

        foreach (var item in _scrEvidenceTextSO.ActEvidence)
        {
            _correctEvi++;
            if (item.ItemName == itemSO.ItemName)
            {
                thisIsRightEvi = true;
                break;
            }
        }

        ChoosingUI.instance.ChkTorF(thisIsRightEvi, _correctEvi,_scrEvidenceTextSO);

        // SO 판별해주는거만 구현하기
        if (thisIsRightEvi)
        {
            Debug.Log("Correct Evidence : " + itemSO);
            // bool 값 넣어줘서 아이템 SO 다 줬는지 판별하기
        }
        else
        {
            Debug.Log("Not Correct Evidence : " + itemSO + ". Please ReSelect Again");
        }
    }
}
