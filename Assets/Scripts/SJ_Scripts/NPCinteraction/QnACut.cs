using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QnACut : interTNPC
{
    // 識澱走澗 鎧析猿走 析舘 析鋼旋昔闇 背鎌生艦
    // 廃 鉢檎雁 廃 蝶遣戚檎 煽係惟 照背亀 喫...
    // 益撹 煽奄拭 益 蝶遣 拝雁背兜生檎 据掘 梅揮企稽 馬檎 喫...
    // 蟹戚什 廃 鉢檎 廃 NPC さささささささ

    [Header("竺舛")]
    public StoryTxtSO _Text;
    [SerializeField] private StorySO _story;
    [SerializeField] private NPCSO npc;

    [Header("蝶遣斗 企紫")]
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
