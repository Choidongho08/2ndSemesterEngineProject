using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobby : MonoBehaviour
{
    [SerializeField] private Transform _setLobbyOptionUI;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Button _lobbyAccessModifyBtn;
    [SerializeField] private TMP_InputField _lobbyNameInput;
    [SerializeField] private CaseBook _caseBook;

    private string _lobbyName;
    private string _caseType;

    public Button createLobbyButton;
    public Button cancelCreateButton;
    public event Action<string> OnCreateLobby;
    public event Action<string> OnLobbyNameChange;
    public bool IsPrivate;

    private void Awake()
    {
        IsPrivate = true;
        _lobbyAccessModifyBtn.onClick.AddListener(() =>
        {
            _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text =
            _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text == "����" ? "�����" : "����";
            IsPrivate = _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text == "����" ? false : true;
        });
        _mainMenu.OnCreateLobby += () => 
        {
            _setLobbyOptionUI.gameObject.SetActive(true); 
            
        };
        createLobbyButton.onClick.AddListener(() =>
        {
            Util.instance.LoadingShow();
            _caseType = _caseBook.caseType;
            if (CheckLobbySetting())
            {
                OnLobbyNameChange?.Invoke(_lobbyName);
                _setLobbyOptionUI.gameObject.SetActive(false);
                OnCreateLobby?.Invoke(_caseType);
            }
        });
        cancelCreateButton.onClick.AddListener(() =>
        {
            ClearCreateLobbyOption();
            _setLobbyOptionUI.gameObject.SetActive(false);
            NowCase.instance.HideText();
            Util.instance.MainMenuShow();
        });
    }

    private bool CheckLobbySetting()
    {
        _lobbyName = Regex.Replace(_lobbyNameInput.text, @"[^0-9a-zA-Z��-�R��-����-��]", "", RegexOptions.Singleline);
        if (!_lobbyNameInput.text.Equals(_lobbyName) || _lobbyName == "")
        {
            Util.instance.LoadingHide();
            Message.instance.SetTitleAndMessageText(ExcelReader.instance.dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.CreateLobbyFail_Name)].name, ExcelReader.instance.dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.CreateLobbyFail_Name)].errorCode);
            Debug.Log("Ư������ �ȵ�! �� ��������");
            _lobbyNameInput.text = string.Empty;
            return false;
        }
        else
        {
            if (_caseType != "Case0")
            {
                Debug.Log("����!");
                return true;
            }
            else
            {
                Debug.Log(_caseType);
                Util.instance.LoadingHide();
                Message.instance.SetTitleAndMessageText(ExcelReader.instance.dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.CreateLobbyFail_Case)].name, ExcelReader.instance.dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.CreateLobbyFail_Case)].errorCode);
                return false;
            }

        }
    }
    public void ClearCreateLobbyOption()
    {
        _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text = "�����";
        IsPrivate = true;
        _lobbyNameInput.text = string.Empty;
        _caseType = "Case0";
        NowCase.instance.SetCaseNumber("0");
    }
}
