using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StorySO/EviTexts")]
public class EvidenceTextSO : ScriptableObject
{
    [Header("�ӽñ� �ѵ� NPC��Ȱ�� ���� ��� ���� ��ȭ(QnA,Important)")]
    public List<ItemSO> ActEvidence; // ĳ���Ͱ� �˸��� ���� ���� 
    public List<string> NoneActTxts; // ActEvidence�� ������ ���� ��
    public List<StoryTxtSO> ActTxts; //������ ���� ��
    public int stopPoint; // ������ ���� ����Ʈ
    public string[] PlayerAndNPC = new string[2]; // �÷��̾�, �� NPC �̸�

    [Header("�̰͵� �ӽ����� ���� ã�� �� �����ؾ��ϴ� ����(Important)")]
    public List<ItemSO> CorrectEvidence; //�´� ����
    public List<string> WrrongEvidenceText; // CorrectEvidence�� ������ ���� ��
    public List<StoryTxtSO> CorrectEvidencText; //������ ���� ��
}
