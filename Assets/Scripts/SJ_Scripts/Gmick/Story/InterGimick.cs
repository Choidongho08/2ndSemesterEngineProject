using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterGimick : MonoBehaviour, StoryEnd
{
    [SerializeField] private GameObject _storyPan;
    [SerializeField] private StorySO _storyList;
    [SerializeField] private GimickSO _gimickSO;

    [Header("�÷��̾� ����")]
    [SerializeField] private TextMeshProUGUI _text;

    private bool ItemIsHere = false;

    private int _storyLine;

    private int _storyListNum = 0;

    public void ItemCheck(string needItem) //string���� �ٲ㼭 ��ư ������ �̸� �޾ƿ��� ����
    {
        foreach (InventorySlot item in Inventory.Instance._inventorySlots)
        {
            if (item._myItem.ItemSO.ItemName == "�� ��")
            {
                ItemIsHere = true;
            }
        }
        if (ItemIsHere)
        {
            _storyListNum++;
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
            Debug.Log("����");
            _gimickSO.player2Watering = true;
        }
        else
        {
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
            Debug.Log("����");
        }
    }

    public void InteractionGimick()
    {
        _storyPan.SetActive(true);
        _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
    }

    public void NextStory()
    {
        _storyLine++;
        if (_storyLine != _storyList.TextList[_storyListNum].ChaTxts.Count)
        {
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
        }
        else
        {
            RollBackStory();
        }
    }

    public void RollBackStory()
    {   
        _storyLine = 0;
        _storyListNum = 0;
        _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
        _storyPan.SetActive(false);  
    }
}
