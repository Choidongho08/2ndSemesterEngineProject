using System;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class MainLobby : MonoBehaviour
{
    private Lobby hostLobby;
    private Lobby joinedLobby;
    private float heartbeatTimer = 15;
    private float lobbyUpdateTimer;
    private string _playerName;
    private string _playerReady;
    private string _lobbyName;

    [SerializeField] private Button _lobbyDelete;
    [SerializeField] private Button _updateLobbyGameMode;
    [SerializeField] private Button _createLobbyBtn;
    [SerializeField] private Button _quickJoinLobby;
    [SerializeField] private CaseBook _caseBook;
    [SerializeField] private CreateLobby _createLobby;

    public event Action<string, string, string> OnLobbyCreate;
    public event Action OnGameStart;

    private void Awake()
    {
        ChangeNameUI.OnChangePlayerNamed += ((playerName) => _playerName = playerName);
        _createLobby.OnLobbyNameChange += ChangeLobbyName;
        _createLobby.createLobbyButton.onClick.AddListener(() => CreateLobby());
    }
    private void Start()
    {
        //lobbyDelete.onClick.AddListener(DeleteLobby);
        //updateLobbyGameMode.onClick.AddListener(() =>
        //{
        //    UpdateLobbyGameMode(GetGameMode());
        //});
        //quickJoinLobby.onClick.AddListener(QuickJoinLobby);
    }
    private void Update()
    {
        HandleLobbyHeartbeat();
        HandleLobbyPullForUpdates();
    }

    private string GetCaseType()
    {
        return _caseBook.caseType;
    }

    private async void HandleLobbyHeartbeat()
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0)
            {
                float heartbeatTimerMax = 15;
                heartbeatTimer = heartbeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }
    private async void HandleLobbyPullForUpdates()
    {
        if (joinedLobby != null)
        {
            lobbyUpdateTimer -= Time.deltaTime;
            if (lobbyUpdateTimer < 0)
            {
                float lobbyUpdateTimerMax = 1.1f;
                lobbyUpdateTimer = lobbyUpdateTimerMax;

                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
                joinedLobby = lobby;
            }
        }
    }
    private void ChangeLobbyName(string newLobbyName)
    {
        _lobbyName = newLobbyName;
        Debug.Log(_lobbyName);
    }
    private async void CreateLobby()
    {
        try
        {
            string lobbyName = _lobbyName;
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = _createLobby.IsPrivate,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    {"CaseBook", new DataObject(DataObject.VisibilityOptions.Public, GetCaseType()) },
                }
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(_lobbyName, 2, createLobbyOptions);

            hostLobby = lobby;
            joinedLobby = lobby;

            Debug.Log($"Created Lobby! {lobby.Name} {lobby.LobbyCode}");
            OnLobbyCreate?.Invoke(lobby.LobbyCode, lobbyName, GetCaseType());
            PrintPlayers(hostLobby);
            _createLobby.gameObject.SetActive(false);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        catch(Exception e) 
        {
            Debug.Log(e);
        }
    }

    private async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT),
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                },
            };
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            Debug.Log($"Lobbies found : {queryResponse.Results.Count}");

            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log($"{lobby.Name}" + " " + lobby.Data["CaseBook"].Value);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions lobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            };
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, lobbyByCodeOptions);
            joinedLobby = lobby;

            Debug.Log($"joined lobby with code {lobbyCode}");
            PrintPlayers(joinedLobby);
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
    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
                    {
                        {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, _playerName) },
                        {"Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, _playerReady) }
                    }
        };
    }

    private void PrintPlayers()
    {
        PrintPlayers(joinedLobby);
    }
    private void PrintPlayers(Lobby lobby)
    {
        Debug.Log("Player in lobby " + lobby.Name + " " + lobby.Data["CaseBook"].Value);
        foreach (Player player in lobby.Players)
        {
            Debug.Log(player.Id + " " + player.Data["PlayerName"].Value);
        }
    }
    private async void UpdateLobbyGameMode(string gameMode)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
                {
                    {"CaseBook", new DataObject(DataObject.VisibilityOptions.Public, GetCaseType()) },
                }
            });

            joinedLobby = hostLobby;
            PrintPlayers(hostLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private async void UpdatePlayerReady(string newPlayerReady)
    {
        try
        {
            _playerReady = newPlayerReady;
            Debug.Log(_playerReady);
            await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
            {
                Data = new Dictionary<string, PlayerDataObject>
                {
                    {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, _playerName) },
                    {"Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, _playerReady) }
                }
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private async void LeaveLobby()
    {
        try
        {
            
            if(Leave())
            {
                MigrateLobbyHost();
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
            }
            else
            {
                DeleteLobby();
            }

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private bool Leave()
    {
        if (joinedLobby.Players.Count >= 2)
            return true;
        else 
            return false;
    }
    private async void KickPlayer()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, joinedLobby.Players[1].Id);

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private async void MigrateLobbyHost()
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                HostId = joinedLobby.Players[1].Id
            });

            joinedLobby = hostLobby;
            PrintPlayers(hostLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private async void DeleteLobby()
    {
        try
        {
            await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private void GameStart()
    {
        int readyCount = 0;
        foreach (Player player in joinedLobby.Players)
        {
            if(player.Data["Ready"].Value == "Ready")
                readyCount++;
        }
        if (readyCount >= 2)
            OnGameStart?.Invoke();
        readyCount = 0;
    }
}
