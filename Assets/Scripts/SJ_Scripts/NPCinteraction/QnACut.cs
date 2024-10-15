using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QnACut : interTNPC
{
    public StoryTxtSO _Text;
    [SerializeField] private StorySO _story;
    [SerializeField] private NPCSO npc;

    [SerializeField] private TextMeshProUGUI _charName;
    [SerializeField] private TextMeshProUGUI _charTxt;

    public int _storyLine;

    private void Awake()
    {
        _charTxt.text = _Text.ChaTxts[0];
        _charName.text = npc.CharName;
        gameObject.GetComponent<Image>().sprite = npc.CharImg;
    }

    private void QnA()
    {
        if(_story.isthisNPCcanQnA || _story.isthisNPCImportant)
        {
            if(_storyLine == _Text.stopPoint)
            {
                // 상태창! 인벤창!
                Debug.Log("상태창 띄우기");
            }
        }
    }
}
