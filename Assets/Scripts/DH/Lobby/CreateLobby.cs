using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobby : MonoBehaviour
{
    private Lobby hostLobby;
    private Lobby joinedLobby;
    [SerializeField]private Button createLobbyBtn;


    private void Start()
    {
        createLobbyBtn.onClick.AddListener(CreatedLobby);
    }
    private async void CreatedLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
                Player = MainLobbyManager.instance.GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, "Mode1") },
                    {"Map", new DataObject(DataObject.VisibilityOptions.Public, "de_dust2") }
                }
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 2, createLobbyOptions);

            MainLobbyManager.instance.hostLobby = lobby;
            MainLobbyManager.instance.joinedLobby = lobby;
            hostLobby = MainLobbyManager.instance.hostLobby;
            joinedLobby = MainLobbyManager.instance.joinedLobby;

            Debug.Log($"Created Lobby! {lobby.Name} {lobby.LobbyCode}");
            MainLobbyManager.instance.PrintPlayers(hostLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
