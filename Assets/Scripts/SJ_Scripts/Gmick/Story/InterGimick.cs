using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterGimick : MonoBehaviour, StoryEnd, ItemCheck
{
    [SerializeField] private GameObject _storyPan;
    [SerializeField] private StorySO _storyList;
    [SerializeField] private GimickSO _gimickSO;

    [Header("플레이어 설정")]
    [SerializeField] private TextMeshProUGUI _text;

    private bool ItemIsHere = false;

    private int _storyLine;

    private int _storyListNum = 0;

    public void ItemCheck()
    {
        foreach (ItemSO item in Inventory.Instance._items)
        {
            if (item.ItemName == "빈 병")
            {
                ItemIsHere = true;
            }
        }
        if (ItemIsHere)
        {
            _storyListNum++;
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
            Debug.Log("있음");
            _gimickSO.player2Watering = true;
        }
        else
        {
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
            Debug.Log("없음");
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
