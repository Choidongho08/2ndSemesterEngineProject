using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StorySO/Texts")]
public class StoryTxtSO : ScriptableObject
{
    [Header("������ ��� Ʋ")]
    public List<string> ChaTxts; // ĳ���� �Ϲ� ���
    public int stopPoint; // ������ ���� ����Ʈ(������ �밭 ū�� ���� 10000000 �ֱ�)
}
