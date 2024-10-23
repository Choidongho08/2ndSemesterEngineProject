using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ChoosingUI : interTNPC
{
    [SerializeField] private QnACut _qnaCut;

    public void JustDoIt()
    {
        //인벤(증거창) 열기
        Debug.Log("인벤창 열어");
    }

    public void DontDoThat()
    {
        Debug.Log("클릭");
        _qnaCut.RollBackStory();
    }
}
