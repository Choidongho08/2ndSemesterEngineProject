using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class TestLobby : MonoBehaviour
{
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CreateLobby();
        }
    }

    private async void CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 2);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
