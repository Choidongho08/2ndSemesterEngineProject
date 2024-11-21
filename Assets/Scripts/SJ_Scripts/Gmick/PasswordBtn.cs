using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordBtn : PuzzleManager
{
    [SerializeField] private GameObject passwordPan;

    public void PanelOpen()
    {
        passwordPan.SetActive(true);
    }

    public void PanelClose()
    {
        passwordPan.SetActive(false);
    }

    public void RadioBtnpassword(string btnNum)
    {
        _gimick.RadioPassWord += btnNum;
        Debug.Log(btnNum);
    }

    public void RadioCheckPassword()
    {
        if(_gimick.RadioPassWord == _gimick.radioGimickPass)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            Debug.Log("틀림");
            ResetNum();
        }
    }

    public void ResetNum()
    {
        _gimick.RadioPassWord = null;
        Debug.Log("리셋");
    }
}
