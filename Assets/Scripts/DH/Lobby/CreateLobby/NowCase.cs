using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NowCase : MonoSingleton<NowCase>
{
    private TextMeshProUGUI _nowCaseTxt;

    private void Awake()
    {
        _nowCaseTxt = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetCaseNumber(string txt)
    {
        _nowCaseTxt.text = $"now case : {txt}";
    }
    public int GetCase(CaseType caseType) => caseType switch 
    {
        CaseType.case1 => 1,
        CaseType.case2 => 2,
        CaseType.case3 => 3,
        CaseType.case4 => 4,
        CaseType.case5 => 5,
        CaseType.case6 => 6,
        _ => throw new Exception("Case not Defined ")
    };

}

public enum CaseType
{
    case1,
    case2,
    case3,
    case4,
    case5,
    case6
}
