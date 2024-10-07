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
}
