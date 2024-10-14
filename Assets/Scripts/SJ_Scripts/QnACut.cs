using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class QnACut : interTNPC
{
    [SerializeField] private StorySO story;

    [SerializeField] private TextMeshProUGUI _charName;
    [SerializeField] private TextMeshProUGUI _charTxt;
    [SerializeField] private Sprite _charImg;

    int _storyLine = 0;

    private void Awake()
    {
        _charTxt.text = story.ChaTxts[0];
        _charName.text = story.CharName;
        _charImg = story.CharImg;
    }

    private void Update()
    {
        NextTxt();
    }

    private void NextTxt()
    {
        if(story.ChaTxts == null)
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            _storyLine++;
            if (_storyLine < story.ChaTxts.Count)
            {
                _charTxt.text = story.ChaTxts[_storyLine];
            }
            else
            {
                _storyLine = 0;
                storyPan.SetActive(false);
            }
        }
    }

    private void QnA()
    {
        if(story.isthisNPCcanQnA || story.isthisNPCImportant)
        {
            if(_storyLine == story.stopPoint)
            {
                // 상태창! 인벤창!
            }
        }
    }
}
