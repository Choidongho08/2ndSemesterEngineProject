using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/NPCSO")]
public class StorySO : ScriptableObject
{
    [Header("ĳ�� ����")]
    public string CharName; // ĳ���� �̸�
    public Sprite CharImg; // ĳ���� �̹���

    [Header("������ ��� ������")]
    public List<string> ChaTxts; // ĳ���� �Ϲ� ���

    [Header("NPC��Ȱ")]
    public bool isthisNPCImportant; // ���ſ� ���� ����, �ɹ� ����
    public bool isthisNPCcanQnA; // ���ſ� ���� ����

    [Header("�ӽñ� �ѵ� NPC��Ȱ�� ���� ��� ���� ��ȭ(QnA,Important)")]
    public List<string> ActEvidence; // ĳ���Ͱ� �˸��� ���� ���� 
    public List<string> NoneActTxts; // ActEvidence�� ������ ���� ��
    public List<string> ActTxts; //������ ���� ��
    public int stopPoint; // ������ ���� ����Ʈ
    public string[] PlayerAndNPC = new string[2]; // �÷��̾�, �� NPC �̸�

    [Header("�̰͵� �ӽ����� ���� ã�� �� �����ؾ��ϴ� ����(Important)")]
    public List<string> CorrectEvidence; //�´� ����
}
