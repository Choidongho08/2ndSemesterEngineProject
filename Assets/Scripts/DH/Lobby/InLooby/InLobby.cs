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
        MainLobby.instance.OnLobbyCreate += (lobbyCode, lobbyName,lobbyCase) => 
        {
            _lobbyCode.text = lobbyCode;
            _lobbyName.text = lobbyName;
            _lobbyCase.text = lobbyCase;
            _lobby.SetActive(true);
            Loading.instance.Hide();
        };
    }
}
