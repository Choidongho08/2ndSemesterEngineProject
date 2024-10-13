using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobby : MonoBehaviour
{
    [SerializeField] private Button _createLobbyBtn;
    [SerializeField] private Transform _setLobbyOptionUI;
    [SerializeField] private MainMenu _mainMenu;

    public bool IsPrivate;

    private void Awake()
    {
        _mainMenu.OnCreateLobby += () => _setLobbyOptionUI.gameObject.SetActive(true);
        _createLobbyBtn.onClick.AddListener(() =>
        {
            _setLobbyOptionUI.gameObject.SetActive(true);
            _createLobbyBtn.gameObject.SetActive(false);
        });
    }
}
