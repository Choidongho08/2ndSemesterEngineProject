using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSelectEvidence : MonoBehaviour
{
    //public List<EvidenceTextSO> EvidenceSO;

    //[SerializeField] private Ease _easyType;
    //[SerializeField] private GameObject _inventory;

    //private Inventory _scrInventory;
    //private InventoryItem _scrInventoryItem;
    //private EvidenceTextSO _scrEvidenceTextSO;
    //private SelectCriminal _scrSelectCriminal;

    //private Vector2 _trInven;

    //private void Awake()
    //{
    //    _trInven = _inventory.GetComponent<RectTransform>().anchoredPosition;
    //    _scrSelectCriminal = FindObjectOfType<SelectCriminal>();
    //}

    //private void Start()
    //{
    //    var inventoryItems = FindObjectsOfType<InventoryItem>();
    //    foreach (var item in inventoryItems)
    //    {
    //        item.OnSubmitEvidence.AddListener(HandleEvidenceSubmission);
    //    }
    //}

    //public void EvidenceSelect()
    //{
    //    Debug.Log("Select Evidence");

    //    _inventory.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 5), 1f).SetEase(_easyType);
    //}

    //public void HandleEvidenceSubmission(ItemSO itemSO)
    //{
    //    if (itemSO != null)
    //    {
    //        Debug.Log("Submitted Evidence : " + itemSO.ItemName);

    //        _inventory.GetComponent<RectTransform>().DOAnchorPos(_trInven, 1f).SetEase(_easyType);

    //        SelectEvidence(itemSO);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No Item selected for evidence submission");
    //    }
    //}

    private void SelectEvidence(ItemSO itemSO)
    {
        // Debug.Log("Select Evidence : " + itemSO.ItemName);

        // for (int i = 0; i < _scrSelectCriminal)
    }
}
