using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharSO/CharInfo")]
public class NPCSO : ScriptableObject
{
    [Header("ĳ�� ����")]
    public string CharName; // ĳ���� �̸�
    public Sprite CharImg; // ĳ���� �̹���
}
