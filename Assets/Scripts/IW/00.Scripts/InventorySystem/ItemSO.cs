using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item So", order = 0)]
public class ItemSO : ScriptableObject
{
    [Header("������ ����")]
    public string ItemName; // �������� ���� ID
    public string ItemInfo; // ������ ����
    public Sprite ItemIcon; // ������ �׸�

    [Header("�÷��̾� ��ȣ�ۿ�")]
    public string[] PlayerID;
    internal Sprite sprite;
}
