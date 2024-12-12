using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;

public class InterGimick : MonoBehaviour, IStoryEnd
{
    [SerializeField] private GameObject _storyPan;
    [SerializeField] private StorySO _storyList;
    [SerializeField] private GimickSO _gimickSO;

    [Header("플레이어 설정")]
    [SerializeField] private TextMeshProUGUI _text;

    public static bool ItemIsHere = false;

    private int _storyLine = 0;

    private int _storyListNum = 0;

    public void ItemCheck(string needItem) //string으로 바꿔서 버튼 누를때 이름 받아오기 각자
    {
        foreach (ItemSO item in Inventory.Instance._collectedItem)
        {
            if (item.ItemName == needItem)
            {
                ItemIsHere = true;
            }
        }
        if (ItemIsHere && !_gimickSO.player2Watering)
        {
            _storyListNum = 1;
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
            _gimickSO.player2Watering = true;
        }
        else if (ItemIsHere && _gimickSO.player2Watering)
        {
            _storyListNum = 2;
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
        }
        else if(!ItemIsHere || !_gimickSO.player2Watering)
        {
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
        }
    }

    public void SceneChangeItemCheck(string needItem) //string으로 바꿔서 버튼 누를때 이름 받아오기 각자
    {
        foreach (ItemSO item in Inventory.Instance._collectedItem)
        {
            if (item.ItemName == needItem)
            {
                ItemIsHere = true;
                break;
            }
        }
        if(!ItemIsHere)
        {
            InteractionGimick();
        }
    }

    public void ChangeImg(Sprite sprite)
    {
        if(ItemIsHere)
            gameObject.GetComponentInChildren<Image>().sprite = sprite;
    }

    public void InteractionGimick()
    {
        _storyListNum = 0;
        _storyPan.SetActive(true);
        _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
    }

    public void StatueInteractionGimick(GameObject password)
    {
        StatueInteractionGimickServerRpc(password);
    }
    [ServerRpc(RequireOwnership = false)]
    private void StatueInteractionGimickServerRpc(GameObject password)
    {
        StatueInteractionGimickClientRpc(password);
    }
    [ClientRpc]
    private void StatueInteractionGimickClientRpc(GameObject password)
    {
        if (!_gimickSO.player2Watering)
        {
            _storyListNum = 0;
            _storyPan.SetActive(true);
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
        }
        else
        {
            _storyListNum = 1;
            _storyPan.SetActive(true);
            _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
            password.SetActive(true);
            GetComponentInChildren<Button>().enabled = false;
        }
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

    public void StoryOff()
    {
        RollBackStory();
    }

    public void RollBackStory()
    {
        _storyLine = 0;
        _storyListNum = 0;
        _text.text = _storyList.TextList[_storyListNum].ChaTxts[_storyLine];
        _storyPan.SetActive(false);  
    }

}
