using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class interTNPC : MonoBehaviour
{
    [SerializeField] protected GameObject storyPan;
    [SerializeField] protected GameObject choosingPan;
    protected bool _QnA = false;
    protected bool _important = false;
    protected int whatNpc;

    private void WhatIsNPC(string tag)
    {
        switch (tag)
        {
            case "NPC(background)":
                Debug.Log("���� ���");
                return;
            case "NPC(QnA)":
                Debug.Log("���� ���� ������");
                return;
            case "NPC(important)":
                Debug.Log("���� ���� ������ �׸��� ���� ����");
                return;
            default:
                Debug.LogError("�� �±� �� �־��ų� �߸� ���� ��? NPC �������� ����");
                return;
        }
    }
    private void StoryOn()
    {
        storyPan.SetActive(true);
    }

    public void InteractiveNPC()
    {
        WhatIsNPC(gameObject.tag);
        StoryOn();
    }
}
