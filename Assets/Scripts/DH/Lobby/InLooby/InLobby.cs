using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InLobby : MonoBehaviour
{
    [SerializeField] private GameObject _lobby;
    [SerializeField] private TextMeshProUGUI _lobbyCode;
    [SerializeField] private TextMeshProUGUI _lobbyName;
    [SerializeField] private TextMeshProUGUI _lobbyCase;

    private void Awake()
    {
        MainLobby.instance.OnLobbyCreate += (lobbyCode, lobbyName, lobbyCase, sprite) => Lobby(lobbyCode, lobbyName, lobbyCase, sprite);
        MainLobby.instance.OnLobbyJoined += (lobbyCode, lobbyName, lobbyCase, sprite) => Lobby(lobbyCode,lobbyName, lobbyCase, sprite);
    }
    private void Lobby(string lobbyCode, string lobbyName, string lobbyCase, Sprite sprite)
    {
        _lobbyCode.text = lobbyCode;
        _lobbyName.text = lobbyName;
        _lobbyCase.text = lobbyCase;
        _lobby.SetActive(true);
        Util.instance.LoadingHide();
        InLobbyUI.instance.CaseImageChange(sprite);
    }
}
