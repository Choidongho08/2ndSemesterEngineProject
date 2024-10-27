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
    [SerializeField] private Button _changePlayerNameButton;
    [SerializeField] private Authenticate authenticate;
    [SerializeField] private GameObject _childGameObject;

    public static event Action OnChangeName;
    public event Action OnQuickJoinLobby;
    public event Action OnJoinLobbyByCode;
    public event Action OnCreateLobby;

    private void Awake()
    {
        _changePlayerNameButton.onClick.AddListener(() => OnChangeName?.Invoke());
        _quickJoinLobbyButton.onClick.AddListener(() => { OnQuickJoinLobby?.Invoke(); _childGameObject.SetActive(false); });
        _joinLobbyByCodeButton.onClick.AddListener(() => { OnJoinLobbyByCode?.Invoke(); _childGameObject.SetActive(false); });
        _createLobbyButton.onClick.AddListener(() => { OnCreateLobby?.Invoke(); _childGameObject.SetActive(false); });
        authenticate.OnAfterAuthenticate += () =>
        {
            Loading.instance.Hide();
            _childGameObject.SetActive(true);
        };

    }

}
