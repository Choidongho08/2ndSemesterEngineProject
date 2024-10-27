using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InLobby : MonoBehaviour
{
    [SerializeField] private GameObject _lobby;
    [SerializeField] private MainLobby _mainLobby;
    [SerializeField] private TextMeshProUGUI _lobbyCode;
    [SerializeField] private TextMeshProUGUI _lobbyName;
    [SerializeField] private TextMeshProUGUI _lobbyCase;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _readyButton;

    public event Action OnStart;
    public event Action OnReady;

    private void Awake()
    {
        _mainLobby.OnLobbyCreate += (lobbyCode, lobbyName,lobbyCase) => 
        {
            _lobbyCode.text = lobbyCode;
            _lobbyName.text = lobbyName;
            _lobbyCase.text = lobbyCase;
            _lobby.SetActive(true);
            Loading.instance.Hide();
        };
    }
}
