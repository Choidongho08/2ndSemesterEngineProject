using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Events;

public class SuggestEvidence : MonoBehaviour
{
    public List<EvidenceTextSO> EvidenceSO;

    [SerializeField] private Ease _easyType;
    [SerializeField] private GameObject _inventory;

    private Inventory _scrInventory;
    private InventoryItem _scrInventoryItem;
    private EvidenceTextSO _scrEvidenceTextSO;
    private SelectCriminal _scrSelectCriminal;

    private Vector2 _trInven;

    private void Awake()
    {
        _trInven = _inventory.GetComponent<RectTransform>().anchoredPosition;
        _scrSelectCriminal = FindObjectOfType<SelectCriminal>();
    }

    private void Start()
    {
        var inventoryItems = FindObjectsOfType<InventoryItem>();
        foreach (var item in inventoryItems)
        {
            item.OnSubmitEvidence.AddListener(HandleEvidenceSubmission);
        }
    }

    public void EvidenceSuggest()
    {
        Debug.Log("Select Evidence");

        _inventory.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 5), 1f).SetEase(_easyType);
    }

    public void HandleEvidenceSubmission(ItemSO itemSO)
    {
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
        // 증거 제출 처리 로직 추가
        Debug.Log("Processing Evidence : " + itemSO.ItemName);

        // SO 판별해주는거만 구현하기
        if (_scrSelectCriminal._objCurrentPanel.GetComponent<scrPutCharSO>()._soChar.ActEvidence[0].ItemName == itemSO.ItemName)
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
