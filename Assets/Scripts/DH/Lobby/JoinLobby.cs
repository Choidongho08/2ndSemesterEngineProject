using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using UnityEngine;

public class JoinLobby : MonoBehaviour
{
    private Lobby joinedLobby;

    private void Start()
    {
        joinedLobby = MainLobbyManager.instance.joinedLobby;
    }
    private async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions lobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = MainLobbyManager.instance.GetPlayer()
            };
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, lobbyByCodeOptions);
            joinedLobby = lobby;

            Debug.Log($"joined lobby with code {lobbyCode}");
            MainLobbyManager.instance.PrintPlayers(joinedLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void QuickJoinLobby()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }
}
