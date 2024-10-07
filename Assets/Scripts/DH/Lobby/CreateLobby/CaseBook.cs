using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CaseBook : MonoBehaviour
{
    [SerializeField] private Transform _caseTemplate;
    [SerializeField] private CaseBookListSO _caseBookListSo;

    private void Awake()
    {
        _caseTemplate.gameObject.SetActive(false);
        foreach (CaseBookSO item in _caseBookListSo.list)
        {
            Transform caseTemplate = Instantiate(_caseTemplate, transform);
            caseTemplate.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            MouseEnterExitEvents mouseEvent = caseTemplate.GetComponent<MouseEnterExitEvents>();
            mouseEvent.OnEnterMouse += () =>
            {
                GetComponentInChildren<Image>().color = Color.gray;
            };
            mouseEvent.OnExitMouse += () =>
            {
                GetComponentInChildren<Image>().color = Color.white;
            };

            caseTemplate.gameObject.SetActive(true);
        }

        
    }

}