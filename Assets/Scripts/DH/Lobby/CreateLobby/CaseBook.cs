using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CaseBook : MonoBehaviour
{
    [SerializeField] private Transform _caseTemplate;
    [SerializeField] private CaseBookListSO _caseBookListSo;

    public string caseType;

    private void Awake()
    {
        _caseTemplate.gameObject.SetActive(false);
        foreach (CaseBookSO item in _caseBookListSo.list)
        {
            Transform caseTemplate = Instantiate(_caseTemplate, transform);
            caseTemplate.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            MouseEnterExitEvents mouseEvent = caseTemplate.GetComponent<MouseEnterExitEvents>();

            mouseEvent.OnEnterMouse += () => { caseTemplate.GetComponentInChildren<Image>().color = Color.gray; };
            mouseEvent.OnExitMouse += () => { caseTemplate.GetComponentInChildren<Image>().color = Color.white; };

            caseTemplate.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                NowCase.instance.SetCaseNumber(item.caseNumber.ToString());
                switch (NowCase.instance.GetCase(item.caseType))
                {
                    case 1:
                        caseType = "Case1";
                        break;
                    case 2:
                        caseType = "Case2";
                        break;
                    case 3:
                        caseType = "Case3";
                        break;
                    case 4:
                        caseType = "Case4";
                        break;
                    case 5:
                        caseType = "Case5";
                        break;      
                    case 6:         
                        caseType = "Case6";
                        break;
                    default:
                        throw new Exception("No case");
                };
            });

            caseTemplate.gameObject.SetActive(true);
        }

        
    }

}