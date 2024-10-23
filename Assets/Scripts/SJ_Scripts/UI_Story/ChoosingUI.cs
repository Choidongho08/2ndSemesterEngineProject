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

        /*try
        {
            인벤창에 뭐라도 있으면 열기
        }
        catch
        {
            하나도 없으면 제시할 아이템이 없음을 알리기
        }*/
    }

    public void DontDoThat()
    {
        Debug.Log("클릭");
        _qnaCut.RollBackStory();
    }
}
