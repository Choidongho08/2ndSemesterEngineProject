using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordBtn : PuzzleManager
{
    public void RadioBtnpassword(string btnNum)
    {
        _gimick.RadioPassWord += btnNum;
    }

    public void RadioCheckPassword()
    {
        if(_gimick.RadioPassWord == _gimick.radioGimickPass)
        {

        }
        else
        {
            Debug.Log("Ʋ��");
            ResetNum();
        }
    }

    public void ResetNum()
    {
        _gimick.RadioPassWord = null;
        Debug.Log("����");
    }
}
