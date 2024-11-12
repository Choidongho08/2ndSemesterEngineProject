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

    private int _storyLine;

    private int _storyListNum = 0;

    public void ItemCheck()
    {
        if (_gimickSO.player2Watering == false)//item.ItemName == "EmptyBottle")
        {
            _storyListNum++;
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
            Debug.Log("있음");
            _gimickSO.player2Watering = true;
        }
        else if(_gimickSO.player2Watering == true)
        {
            _storyListNum += 2;
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
        }
        else
        {
            //RollBackStory();
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
            Debug.Log("없음");
        }
        /*//foreach (ItemSO item in Inventory.Instance._items)
        {
            if(item.ItemName == "EmptyBottle")
            {
                // 넣을거 넣기
            }
        }*/
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
