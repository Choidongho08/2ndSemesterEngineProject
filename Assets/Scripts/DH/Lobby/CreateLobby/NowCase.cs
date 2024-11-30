using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class NowCase : MonoSingleton<NowCase>
{
    [SerializeField] private CaseBook _caseBook;
    [SerializeField]private TextMeshProUGUI _nowCaseTxt;

    private int caseNumber;

    public void SetCaseNumber(string txt)
    {
        Debug.Log(txt);
        caseNumber = int.Parse(txt);
        Debug.Log(caseNumber);
        _nowCaseTxt.gameObject.SetActive(true);
        _nowCaseTxt.text = $"��� ��ȣ : {txt}";
        _caseBook.caseType = $"Case{txt}";
    }
    public int CaseNumber()
    {
        return caseNumber;
    }
    public void HideText()
    {
        _nowCaseTxt.gameObject.SetActive(false);
    }
    public void ShowText()
    {
        _nowCaseTxt.gameObject.SetActive(true);
    }
    public int GetCase(CaseType caseType) => caseType switch 
    {
        CaseType.Case1 => 1,
        CaseType.Case2 => 2,
        CaseType.Case3 => 3,
        CaseType.Case4 => 4,
        CaseType.Case5 => 5,
        CaseType.Case6 => 6,
        _ => throw new Exception("Case not Defined ")
    };

}

public enum CaseType
{
    Case1,
    Case2,
    Case3,
    Case4,
    Case5,
    Case6
}
