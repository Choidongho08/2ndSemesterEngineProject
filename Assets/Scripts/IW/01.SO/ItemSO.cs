using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item So", order = 0)]
public class ItemSO : ScriptableObject
{
    [Header("������ ����")]
    public string ItemName; // �������� ���� ID

    [Header("�÷��̾� ��ȣ�ۿ�")]
    public string[] PlayerID;
}
