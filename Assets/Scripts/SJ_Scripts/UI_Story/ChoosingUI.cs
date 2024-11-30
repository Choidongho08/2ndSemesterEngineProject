using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoosingUI : MonoBehaviour
{
    public static ChoosingUI instance;
    [SerializeField] private GameObject talkingPan;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI npcText;
    private StoryTxtSO nowStory;
    public int _storyLine;

    private void Start()
    {
        if (instance = null)
            instance = this;
    }

    private void SetCharSO(EvidenceTextSO charinfo)
    {
        npcName.text = charinfo.NPC;
        talkingPan.SetActive(true);
    }

    private void RollBackStory()
    {
        _storyLine = 0;
        talkingPan.SetActive(false);
    }

    public void ChkTorF(bool TorF, int eviNum, EvidenceTextSO charText)
    {
        if (TorF)
        {
            nowStory = charText.ActTxts[eviNum];
            npcText.text = nowStory.ChaTxts[0];
            SetCharSO(charText);
        }
        else
        {
            nowStory = charText.NoneActTxts;
            npcText.text = charText.NoneActTxts.ChaTxts[0];
            SetCharSO(charText);
        }
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
