using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextText : interTNPC
{
    [SerializeField] private QnACut _storyLine;
    [SerializeField] private StoryTxtSO _story;

    [SerializeField] private TextMeshProUGUI _charText;

    private void Awake()
    {
        _story = _storyLine._Text;
    }

    public void NextStory()
    {
        _storyLine._storyLine++;
        if(_storyLine._storyLine != _story.ChaTxts.Count)
        {
            _charText.text = _story.ChaTxts[_storyLine._storyLine];
        }
        else
        {
            _storyLine._storyLine = 0;
            _charText.text = _story.ChaTxts[_storyLine._storyLine];
            storyPan.SetActive(false);
        }
    }
}
