using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gimick/SO")]
public class GimickSO : ScriptableObject
{
    [Header("���� �� �����")]
    public string flickPuzzlePassword; // �� ���
    public bool puzzleSet = false; // �����Ǹ� �ٽô� �� �ٲٰ� �ϴ°�
    public string numberCheck; // �𽺺�ȣ ��ȯ�� ��� (�Է��� �ǵ��� ��ȯ)
    public bool puzzleEnd = false; // ���� �ذ�

    [Header("�� ���� �� ���")]
    public bool itCantEnter = false; // true�� �̹��� �ٲٰ� �� �� �ְ�
    public bool player1HasScissor = false; // �÷��̾� ������ ����
    public bool player2HasScissor = false; // �÷��̾� ������ ����

    [Header("���� ��� �� ���")]
    public bool player1HasUm = false; // true�� ����� ������ ����
    public bool player2HasWater = false; // true�� ���� �ο��� ���� ����
    public bool player2Watering = false; // true�� �̹� ���� �ο� ��Ȳ
    public bool StatueHasUm = false; // ��� ������ �ִ��� (�Ʒ��� StatBlockR�ӽñ�� ����)
    public bool StatueBlockRain = false; // �� ������ ���� �ִ���

    [Header("����, ����Ŀ �� ���")]
    [Range(1,5)] public int radioChannel; // ���� ���� ������ �Ҹ��� �ٸ�
    public List<AudioSource> sound; // ���� ���� ���� �Ҹ�
    public bool puzzleDone = false; // ������ �ذ� �Ǿ�����

    [Header("�����")]
    public string radioGimickPass; //���� ��� ��й�ȣ
    public string StatuGimickPass; //���� ��� ��й�ȣ
    public string RadioPassWord; //���� ��� ��й�ȣ �Է�
    public string StatuPassWord; //���� ��� ��й�ȣ �Է�
}

public interface ItemCheck
{
    void ItemCheck();
}
