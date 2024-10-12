using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CaseBook : MonoBehaviour
{
    [SerializeField] private Transform _caseTemplate;
    [SerializeField] private CaseBookListSO _caseBookListSo;

    public string _caseType;

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
                _caseType = NowCase.instance.GetCase(item.caseType);
            });

            caseTemplate.gameObject.SetActive(true);
        }

        
    }

}