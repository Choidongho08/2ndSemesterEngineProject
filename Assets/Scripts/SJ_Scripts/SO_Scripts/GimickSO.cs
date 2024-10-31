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
    public bool playerHasUm = false; // true�� ����� ������ ����
    public bool playerHasWater = false; // true�� ���� �ο��� ���� ����
    public bool StatueHasUm = false; // ��� ������ �ִ��� (�Ʒ��� StatBlockR�ӽñ�� ����)
    public bool StatueBlockRain = false; // �� ������ ���� �ִ���

    [Header("����, ����Ŀ �� ���")]
    public int radioChannel; // ���� ���� ������ �Ҹ��� �ٸ�
    public bool puzzleDone = false; // ������ �ذ� �Ǿ�����
}
