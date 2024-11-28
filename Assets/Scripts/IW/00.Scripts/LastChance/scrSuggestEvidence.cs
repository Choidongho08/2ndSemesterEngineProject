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
            Debug.Log("Added Listener");
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
        _isEvidenceCorrect = true;

        // 증거 제출 처리 로직 추가
        Debug.Log("Processing Evidence : " + itemSO.ItemName);

        for (int i = 0; i < _scrSelectCriminal._objCurrentPanel.GetComponent<scrPutCharSO>()._soChar.ActEvidence.Count; i++)
        {
            // SO 판별해주는거만 구현하기
            if (_scrSelectCriminal._objCurrentPanel.GetComponent<scrPutCharSO>()._soChar.ActEvidence[i].ItemName == itemSO.ItemName && _isEvidenceCorrect)
            {
                Debug.Log("Correct Evidence : " + itemSO);

                return; 
            }
            else
            {
                Debug.Log("Not Correct Evidence : " + itemSO + ". Please ReSelect Again");
            }
        }
    }
}
