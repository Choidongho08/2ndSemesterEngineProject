using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickLight : PuzzleManager
{
    [SerializeField] private Button BulbSwitch;
    [SerializeField] private Button LEDSwitch;

    [SerializeField] private string puzzleAns;
    [SerializeField] int a = 0;
    [SerializeField] private GameObject password;

    private event Action puzzleDone;

    public void FlickEbulb()
    {
        Debug.Log("���� ����");
        puzzleAns += "0";
        FlickChecker();
        EndChecking();
    }

    private void OnEnable()
    {
        puzzleDone += ShowPassWord;
    }

    public void FlickLED()
    {
        Debug.Log("LED ����");
        puzzleAns += "1";
        FlickChecker();
        EndChecking();
    }

    private void FlickChecker()
    {

        if (_gimick.numberCheck[a] == puzzleAns[a])
            a++;
        else
        {
            a = 0;
            puzzleAns = null;
            Debug.Log("�ʱ�ȭ");
        }
    }

    private void EndChecking()
    {
        if (_gimick.numberCheck.Length == a)
        {
            _gimick.puzzleEnd = true;
            Debug.Log("���� ��");
            puzzleDone?.Invoke();
            BulbSwitch.interactable = false;
            LEDSwitch.interactable = false;
        }
    }

    private void ShowPassWord()
    {
        password.SetActive(true);
    }
}
