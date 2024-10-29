using System;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class MainLobby : MonoSingleton<MainLobby>
{
    private Lobby hostLobby;
    private Lobby joinedLobby;
    private float heartbeatTimer = 15;
    private float lobbyUpdateTimer;
    private string _playerName;
    private string _playerReady = "False";
    private string _lobbyName;

    [SerializeField] private Button _lobbyDelete;
    [SerializeField] private Button _updateLobbyGameMode;
    [SerializeField] private Button _createLobbyBtn;
    [SerializeField] private Button _quickJoinLobby;
    [SerializeField] private CaseBook _caseBook;
    [SerializeField] private CreateLobby _createLobby;
    
    public class LobbyEventArgs : EventArgs
    {
        public Lobby lobby;
    }
    public event EventHandler OnLeftLobby;
    public event EventHandler<LobbyEventArgs> OnJoinedLobby;
    public event EventHandler<LobbyEventArgs> OnJoinedLobbyUpdate;
    public event EventHandler<LobbyEventArgs> OnKickedFromLobby;
    public string KeyPlayerName = "PlayerName";
    public string KeyPlayerReady = "PlayerReady";
    public string KeyCaseBook = "CaseBook";
    public event Action<string, string, string> OnLobbyCreate;
    public event Action OnGameStart;
    public string PlayerReady
    {
        get
        {
            return _playerReady;
        }
        set
        {
            if (value == null)
                return;
            _playerReady = value;
        }
    }

    public enum PlayerReadyEnum
    {
        True,
        False
    }
    private void Awake()
    {
        _createLobby.OnLobbyNameChange += ChangeLobbyName;
        _createLobby.OnCreateLobby += (CaseType caseType) => CreateLobby(caseType);
        
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


    private async void HandleLobbyHeartbeat()
    {
        if (IsLobbyHost())
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

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

                if (!IsPlayerInLobby())
                {
                    // Player was kicked out of this lobby
                    Debug.Log("Kicked from Lobby!");

                    OnKickedFromLobby?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

                    joinedLobby = null;
                }
            }

        }
    }
    private void ChangeLobbyName(string newLobbyName)
    {
        _lobbyName = newLobbyName;
        Debug.Log(_lobbyName);
    }
    private async void CreateLobby(CaseType caseType)
    {
        Debug.Log(_createLobby.IsPrivate);
        try
        {
            string lobbyName = _lobbyName;
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsLocked = _createLobby.IsPrivate,
                IsPrivate = _createLobby.IsPrivate,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    {KeyCaseBook, new DataObject(DataObject.VisibilityOptions.Public, caseType.ToString(), DataObject.IndexOptions.S1) },
                },
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(_lobbyName, 2, createLobbyOptions);

            joinedLobby = lobby;
            hostLobby = lobby;

            Debug.Log($"Created Lobby! {lobby.Name} {lobby.LobbyCode}");
            OnLobbyCreate?.Invoke(lobby.LobbyCode, lobbyName, caseType.ToString());
            _createLobby.gameObject.SetActive(false);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinLobbyByCode(string lobbyCode)
    {

        Player player = GetPlayer();

        Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, new JoinLobbyByCodeOptions
        {
            Player = player
        });

        joinedLobby = lobby;

        OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });

    }

    public async void QuickJoinLobby()
    {
        try
        {
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();

            options.Filter = new List<QueryFilter>()
            {
                new QueryFilter(field : QueryFilter.FieldOptions.S1 , value : "case0", op: QueryFilter.OpOptions.NE),
            };
            Lobby lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
            joinedLobby = lobby;

            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
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
                        {KeyPlayerName, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, value : _playerName) },
                        {KeyPlayerReady, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, value : _playerReady) }
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
    public void ChangeGameMode()
    {
        if (IsLobbyHost())
        {
            CaseType caseType =
                Enum.Parse<CaseType>(joinedLobby.Data[KeyCaseBook].Value);

            switch (caseType)
            {
                default:
                case CaseType.case1:
                    caseType = CaseType.case1;
                    break;
                case CaseType.case2:
                    caseType = CaseType.case2;
                    break;
            }

            UpdateLobbyGameMode(caseType);
        }
    }
    private async void UpdateLobbyGameMode(CaseType caseBook)
    {
        try
        {   
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
                {
                    {KeyCaseBook, new DataObject(DataObject.VisibilityOptions.Public, caseBook.ToString(), DataObject.IndexOptions.S1) },
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
    public async void UpdatePlayerReady(string newPlayerReady)
    {
        _playerReady = newPlayerReady;
        Debug.Log(_playerReady);
        if (joinedLobby != null)
        {
            try
            {
                UpdatePlayerOptions options = new UpdatePlayerOptions();

                options.Data = new Dictionary<string, PlayerDataObject>
                {
                    { KeyPlayerName, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, value : _playerName) },
                    { KeyPlayerReady, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, value : _playerReady) }
                };

                string playerId = AuthenticationService.Instance.PlayerId;

                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
                joinedLobby = lobby;
                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }
    public async void UpdatePlayerName(string newPlayerName)
    {
        _playerName = newPlayerName;
        if (joinedLobby != null)
        {
            try
            {
                UpdatePlayerOptions options = new UpdatePlayerOptions();

                options.Data = new Dictionary<string, PlayerDataObject>
                {
                    { KeyPlayerName, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, _playerName) },
                    { KeyPlayerReady, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, _playerReady) }
                };

                string playerId = AuthenticationService.Instance.PlayerId;

                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
                joinedLobby = lobby;
                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }

    }
    public async void LeaveLobby()
    {
        if (joinedLobby != null)
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);

                joinedLobby = null;

                OnLeftLobby?.Invoke(this, EventArgs.Empty);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }
    private async void KickPlayer(string playerId)
    {
        if (IsLobbyHost())
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
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
    private void GameStart()
    {
        int readyCount = 0;
        foreach (Player player in joinedLobby.Players)
        {
            if (player.Data[KeyPlayerReady].Value == "true")
                readyCount++;
        }
        if (readyCount >= 2)
            OnGameStart?.Invoke();
        readyCount = 0;
    }
    public bool IsLobbyHost()
    {
        return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
    }
    public Lobby GetJoinedLobby()
    {
        return joinedLobby;
    }
    private bool IsPlayerInLobby()
    {
        if (joinedLobby != null && joinedLobby.Players != null)
        {
            foreach (Player player in joinedLobby.Players)
            {
                if (player.Id == AuthenticationService.Instance.PlayerId)
                {
                    // This player is in this lobby
                    return true;
                }
            }
        }
        return false;
    }
}
