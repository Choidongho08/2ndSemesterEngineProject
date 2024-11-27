using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QnACut : interTNPC, IStoryEnd
{
    [Header("설정")]
    public StoryTxtSO _Text;
    [SerializeField] private StorySO _story;
    [SerializeField] private NPCSO npc;

    [Header("캐릭터 대사")]
    [SerializeField] private TextMeshProUGUI _charName;
    public TextMeshProUGUI _charTxt;

    public int _storyLine;

    private void Awake()
    {
        _charTxt.text = _Text.ChaTxts[0];
        _charName.text = npc.CharName;
        gameObject.GetComponent<Image>().sprite = npc.CharImg;
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
    }
}

