using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterGimick : MonoBehaviour, StoryEnd
{
    [SerializeField] private GameObject _storyPan;
    [SerializeField] private StoryTxtSO _story;

    [Header("플레이어 설정")]
    [SerializeField] private TextMeshProUGUI _text;

    private int _storyLine;

    public void InteractionGimick()
    {
        _storyPan.SetActive(true);
        _text.text = _story.ChaTxts[_storyLine];
    }

    public void NextStory()
    {
        _storyLine++;
        if (_storyLine != _story.ChaTxts.Count)
        {
            _text.text = _story.ChaTxts[_storyLine];
        }
        else
        {
            RollBackStory();
        }
    }

    public void RollBackStory()
    {   
        _storyLine = 0;
        _text.text = _story.ChaTxts[_storyLine];
        _storyPan.SetActive(false);  
    }
}
