using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
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
    public event Action<CaseType> OnJoiendLobby;
    public event Action<string> OnLobbyNameChange;
    public bool IsPrivate;

    private void Awake()
    {
        IsPrivate = true;
        _lobbyAccessModifyBtn.onClick.AddListener(() =>
        {
            _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text =
            _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text == "Public" ? "Private" : "Public";
            IsPrivate = _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text == "Public" ? false : true;
        });
        _mainMenu.OnCreateLobby += () => _setLobbyOptionUI.gameObject.SetActive(true);
        createLobbyButton.onClick.AddListener(() =>
        {
            Util.instance.LoadingShow();
            if (CheckLobbyName())
            {
                OnLobbyNameChange?.Invoke(_lobbyName);
                _setLobbyOptionUI.gameObject.SetActive(false);
                _caseType = _caseBook.caseType;
                OnCreateLobby?.Invoke(_caseType);
            }
        });
        cancelCreateButton.onClick.AddListener(() =>
        {
            _setLobbyOptionUI.gameObject.SetActive(false);
            Util.instance.MainMenuShow();
        });
    }

    private bool CheckLobbyName()
    {
        _lobbyName = Regex.Replace(_lobbyNameInput.text, @"[^0-9a-zA-Z°¡-ÆR]", "", RegexOptions.Singleline);
        if(!_lobbyNameInput.text.Equals(_lobbyName) || _lobbyName == "")
        {
            Util.instance.LoadingHide();
            Debug.Log("Æ¯¼ö¹®ÀÚ ¾ÈµÅ! ÀÌ ¸ÓÀú¸®¾ß");
            _lobbyNameInput.text = string.Empty;
            return false;
        }
        else
        {
            Debug.Log("¼º°ø!");
            return true;
        }
    } 
    public void ClearCreateLobbyOption()
    {
        _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Private";
        IsPrivate = true;
        _lobbyNameInput.text = string.Empty;
        _caseType = "0";
        NowCase.instance.SetCaseNumber("0");
    }
}
