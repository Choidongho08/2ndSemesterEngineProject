using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordBtn : PuzzleManager
{
    [SerializeField] private GameObject passwordPan;
    [SerializeField] private GameObject TrueBill;

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
    }

    public void RadioCheckPassword()
    {
        if(_gimick.RadioPassWord == _gimick.radioGimickPass)
        {
            gameObject.GetComponent<Button>().interactable = false;
            PanelClose();
            TrueBill.SetActive(true);
        }
        else
        {
            ResetNum();
        }
    }

    public void ResetNum()
    {
        _gimick.RadioPassWord = null;
        Debug.Log("¸®¼Â");
    }
}
