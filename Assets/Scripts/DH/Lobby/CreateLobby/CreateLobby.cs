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

    private string _lobbyName;

    public Button createLobbyButton;
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
            Loading.instance.Show();
            if (CheckLobbyName())
            {
                OnLobbyNameChange?.Invoke(_lobbyName);
                _setLobbyOptionUI.gameObject.SetActive(false);
            }
        });
    }

    private bool CheckLobbyName()
    {
        _lobbyName = _lobbyNameInput.text;
        _lobbyNameInput.text = Regex.Replace(_lobbyName, @"[^0-9a-zA-Z°¡-ÆR]", string.Empty);
        if(_lobbyNameInput.text == string.Empty)
        {
            Loading.instance.Hide();
            Debug.Log("Æ¯¼ö¹®ÀÚ ¾ÈµÅ! ÀÌ ¸ÓÀú¸®¾ß");
            return false;
        }
        else
        {
            Debug.Log("¼º°ø!");
            return true;
        }
    } 
}
