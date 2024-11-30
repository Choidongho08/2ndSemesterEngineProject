using System;
using UnityEngine;
using UnityEngine.UI;

public class CaseBook : MonoBehaviour
{
    [SerializeField] private Transform _caseTemplate;
    [SerializeField] private CaseBookListSO _caseBookListSo;

    private Sprite _caseSprite;
    private int _caseNumber;

    public string caseType = "Case0";

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
                        _caseSprite = item.sprite;
                        break;
                    case 2:
                        caseType = "Case2";
                        _caseSprite = item.sprite;
                        break;
                    case 3:
                        caseType = "Case3";
                        _caseSprite = item.sprite;
                        break;
                    case 4:
                        caseType = "Case4";
                        _caseSprite = item.sprite;
                        break;
                    case 5:
                        caseType = "Case5";
                        _caseSprite = item.sprite;
                        break;
                    case 6:
                        caseType = "Case6";
                        _caseSprite = item.sprite;
                        break;
                    default:
                        throw new Exception("No case");
                };
            });
            if (item.isBlock)
                Instantiate(item.lockImage, caseTemplate);
            caseTemplate.gameObject.SetActive(true);
        }

    }
    public Sprite GetCaseSprite(string caseString)
    {
        switch (caseString)
        {
            case "Case1":
                _caseNumber = 1;
                break;
            case "Case2":
                _caseNumber = 2;
                break;
            case "Case3":
                _caseNumber = 3;
                break;
            case "Case4":
                _caseNumber = 4;
                break;
            case "Case5":
                _caseNumber = 5;
                break;
            case "Case6":
                _caseNumber = 6;
                break;
        }
        foreach (var item in _caseBookListSo.list)
        {
            if (_caseNumber == item.caseNumber)
                _caseSprite = item.sprite;
        }
        return _caseSprite;
    }

}