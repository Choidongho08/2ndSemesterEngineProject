using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoosingUI : MonoBehaviour
{
    [SerializeField] private GameObject talkingPan;
    [SerializeField] private TextMeshProUGUI npcText;
    private StoryTxtSO nowStory;
    public int _storyLine;


    private void Update()
    {
        nowStory = scrSuggestEvidence.Instance.nowStory;
    }

    private void RollBackStory()
    {
        _storyLine = 0;
        nowStory = null;
        talkingPan.SetActive(false);
    }

    public void NextStory()
    {
        _storyLine++;
        if (_storyLine != nowStory.ChaTxts.Count)
        {
            npcText.text = nowStory.ChaTxts[_storyLine];
        }
        else
        {
            RollBackStory();
        }
    }
}
