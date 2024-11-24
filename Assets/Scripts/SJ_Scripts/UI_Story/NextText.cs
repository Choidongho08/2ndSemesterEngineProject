using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextText : interTNPC
{
    [SerializeField] private QnACut _storyLine;
    [SerializeField] private StorySO _npcRoll;
    [SerializeField] private StoryTxtSO _story;


    private void Awake()
    {
        _story = _storyLine._Text;
    }

    public void NextStory()
    {
        _storyLine._storyLine++;
        if(_storyLine._storyLine != _story.ChaTxts.Count)
        {
            _storyLine._charTxt.text = _story.ChaTxts[_storyLine._storyLine];
        }
        else
        {
            _storyLine.RollBackStory();
        }
    }
}
