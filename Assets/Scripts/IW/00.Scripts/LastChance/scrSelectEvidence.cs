using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scrSelectEvidence : MonoBehaviour
{
    public List<EvidenceTextSO> EvidenceSO;

    [SerializeField] private Ease _easyType;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject talkingPan;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI npcText;

    private Inventory _scrInventory;
    private InventoryItem _scrInventoryItem;
    private EvidenceTextSO _scrEvidenceTextSO;
    private SelectCriminal _scrSelectCriminal;
    public StoryTxtSO nowStory;

    private Vector2 _trInven;

    private bool _isEvidenceCorrect;
    private int _correctEvi;

    public static scrSelectEvidence Instance { get; private set; }

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
    }

    public void EvidenceSelect()
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

            SelectEvidence(itemSO);
        }
        else
        {
            Debug.LogWarning("No Item selected for evidence submission");
        }
    }

    private void SelectEvidence(ItemSO itemSO)
    {
        Debug.Log("Select Evidence : " + itemSO.ItemName);

        bool thisIsRightEvi = false;
        _correctEvi = 0;

        // 증거 제출 처리 로직 추가
        Debug.Log("Processing Evidence : " + itemSO.ItemName);

        foreach (var item in _scrEvidenceTextSO.CorrectEvidence) // correct change
        {
            _correctEvi++;
            if (item.ItemName == itemSO.ItemName)
            {
                thisIsRightEvi = true;
                Debug.Log(item.ItemName + _correctEvi);
                break;
            }
        }

        // SO 판별해주는거만 구현하기
        if (thisIsRightEvi)
        {
            Debug.Log("Correct Evidence : " + itemSO);
            // bool 값 넣어줘서 아이템 SO 다 줬는지 판별하기
            nowStory = _scrEvidenceTextSO.CorrectEvidencText[_correctEvi - 1]; // correctTxts change
            npcText.text = nowStory.ChaTxts[0];
            SetCharSO(_scrEvidenceTextSO);
        }
        else
        {
            Debug.Log("Not Correct Evidence : " + itemSO + ". Please ReSelect Again");

            nowStory = _scrEvidenceTextSO.WrrongEvidenceText;
            npcText.text = _scrEvidenceTextSO.WrrongEvidenceText.ChaTxts[0]; // WarrerEvidence change
            SetCharSO(_scrEvidenceTextSO);
        }
    }

    private void SetCharSO(EvidenceTextSO charinfo)
    {
        npcName.text = charinfo.NPC;
        talkingPan.SetActive(true);
    }
}
