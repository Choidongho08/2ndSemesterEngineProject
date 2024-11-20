using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "StorySO/storyManager")]
public class StorySO : ScriptableObject
{
    [Header("적당한 대사 모음집")]
    public List<StoryTxtSO> TextList; // 캐릭터 일반 대사

    [Header("NPC역활")]
    public bool isthisNPCImportant; // 증거에 관한 질문, 심문 가능
    public bool isthisNPCcanQnA; // 증거에 관한 질문
}
