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
        //�κ�(����â) ����
        Debug.Log("�κ�â ����");
    }

    public void DontDoThat()
    {
        Debug.Log("Ŭ��");
        _qnaCut.RollBackStory();
    }
}
