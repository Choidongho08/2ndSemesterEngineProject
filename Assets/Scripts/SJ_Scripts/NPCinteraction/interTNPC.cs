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
                Debug.Log("대충 대사");
                return;
            case "NPC(QnA)":
                Debug.Log("대충 대사와 선택지");
                return;
            case "NPC(important)":
                Debug.Log("대충 대사와 선택지 그리고 증거 제시");
                return;
            default:
                Debug.LogError("님 태그 안 넣었거나 잘못 넣은 듯? NPC 관련으로 넣자");
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
