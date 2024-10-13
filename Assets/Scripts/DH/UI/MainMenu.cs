using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _quickJoinLobbyButton;
    [SerializeField] private Button _joinLobbyByCodeButton;
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Image _inputPlayerName;
    [SerializeField] private Authenticate authenticate;

    private List<GameObject> menuUI = new List<GameObject>();

    public event Action OnQuickJoinLobby;
    public event Action OnJoinLobbyByCode;
    public event Action OnCreateLobby;

    private void Awake()
    {
        menuUI.Add(_inputPlayerName.gameObject);
        menuUI.Add(_createLobbyButton.gameObject);
        menuUI.Add(_joinLobbyByCodeButton.gameObject);
        menuUI.Add(_quickJoinLobbyButton.gameObject);

        _quickJoinLobbyButton.onClick.AddListener(() => OnQuickJoinLobby?.Invoke());
        _joinLobbyByCodeButton.onClick.AddListener(() => OnJoinLobbyByCode?.Invoke());
        _createLobbyButton.onClick.AddListener(() => OnCreateLobby?.Invoke());

        authenticate.OnAfterAuthenticate += () =>
        {
            foreach (var item in menuUI)
            {
                item.gameObject.SetActive(true);
            }
        };
        foreach(var item in menuUI)
        {
            item.gameObject.SetActive(false);
            if(item.GetComponent<Button>() != null)
            {
                item.GetComponent<Button>().onClick.AddListener(Hide);
            }
        }
    }
    private void Hide()
    {
        foreach (var item in menuUI)
        {
            item.gameObject.SetActive(false);
        }
    }

}
