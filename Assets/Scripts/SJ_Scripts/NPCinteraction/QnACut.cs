using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QnACut : interTNPC
{
    // 선택지는 내일까지 일단 일반적인건 해놨으니
    // 한 화면당 한 캐릭이면 저렇게 안해도 됨...
    // 그냥 저기에 그 캐릭 할당해놓으면 원래 했던대로 하면 됨...
    // 나이스 한 화면 한 NPC ㅅㅅㅅㅅㅅㅅㅅ

    [Header("설정")]
    public StoryTxtSO _Text;
    [SerializeField] private StorySO _story;
    [SerializeField] private NPCSO npc;

    [Header("캐릭터 대사")]
    [SerializeField] private TextMeshProUGUI _charName;
    public TextMeshProUGUI _charTxt;

    public int _storyLine;

    private void Start()
    {
        _charTxt.text = _Text.ChaTxts[0];
        _charName.text = npc.CharName;
        gameObject.GetComponent<Image>().sprite = npc.CharImg;
    }


    public void QnA()
    {
        if(_story.isthisNPCcanQnA || _story.isthisNPCImportant)
        {
            choosingPan.SetActive(true);
        }
        else
        {
            RollBackStory();
        }
    }

    public void EndOfQnA()
    {
        RollBackStory();
    }


    public void RollBackStory()
    {
        _storyLine = 0;
        _charTxt.text = _Text.ChaTxts[_storyLine];
        storyPan.SetActive(false);
        choosingPan.SetActive(false);
        _QnA = false;
        _important = false;
    }
}
