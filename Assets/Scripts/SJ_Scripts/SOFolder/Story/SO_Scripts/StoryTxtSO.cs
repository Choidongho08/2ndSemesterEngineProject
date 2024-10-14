using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StorySO/Texts")]
public class StoryTxtSO : ScriptableObject
{
    [Header("������ ��� Ʋ")]
    public List<string> ChaTxts; // ĳ���� �Ϲ� ���
    public int stopPoint; // ������ ���� ����Ʈ(������ �밭 ū�� ���� 10000000 �ֱ�)

    [Header("NPC��Ȱ")]
    public bool isthisNPCImportant; // ���ſ� ���� ����, �ɹ� ����
    public bool isthisNPCcanQnA; // ���ſ� ���� ����

    [Header("�ӽñ� �ѵ� NPC��Ȱ�� ���� ��� ���� ��ȭ(QnA,Important)")]
    public List<string> ActEvidence; // ĳ���Ͱ� �˸��� ���� ���� 
    public List<string> NoneActTxts; // ActEvidence�� ������ ���� ��
    public List<string> ActTxts; //������ ���� ��
    public string[] PlayerAndNPC = new string[2]; // �÷��̾�, �� NPC �̸�

    [Header("�̰͵� �ӽ����� ���� ã�� �� �����ؾ��ϴ� ����(Important)")]
    public List<string> CorrectEvidence; //�´� ����
}
