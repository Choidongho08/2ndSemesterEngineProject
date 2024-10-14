using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interTNPC : MonoBehaviour
{
    [SerializeField] protected GameObject storyPan;

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
    private void StoryOnOff()
    {
        storyPan.SetActive(true);
    }

    public void InteractiveNPC()
    {
        WhatIsNPC(gameObject.tag);
        StoryOnOff();
    }
}
