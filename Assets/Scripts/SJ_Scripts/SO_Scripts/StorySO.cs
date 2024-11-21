using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "StorySO/storyManager")]
public class StorySO : ScriptableObject
{
    [Header("������ ��� ������")]
    public List<StoryTxtSO> TextList; // ĳ���� �Ϲ� ���

    [Header("NPC��Ȱ")]
    public bool isthisNPCImportant; // ���ſ� ���� ����, �ɹ� ����
    public bool isthisNPCcanQnA; // ���ſ� ���� ����
}
