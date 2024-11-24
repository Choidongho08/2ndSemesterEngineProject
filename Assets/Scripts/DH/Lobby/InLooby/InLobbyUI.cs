using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class InLobbyUI : MonoSingleton<InLobbyUI>
{
    [SerializeField] private Transform playerSingleTemplate;
    [SerializeField] private Transform container;
    [SerializeField] private Button leaveLobbyButton;
    [SerializeField] private Button _readyButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private PlayerSO _playerSo;

    private InLobby _inLobby;

    private void Awake()    
    {
        _inLobby = GetComponentInParent<InLobby>();
        playerSingleTemplate.gameObject.SetActive(false);

        leaveLobbyButton.onClick.AddListener(() => {
            MainLobby.instance.LeaveLobby();
            Util.instance.LoadingShow();
        });
        _readyButton.onClick.AddListener(() => 
        {
            _playerSo.playerReady = MainLobby.instance.PlayerReady == "False" ? "True" : "False";
            MainLobby.instance.UpdatePlayerReady(_playerSo.playerReady);
        });
        _startButton.onClick.AddListener(() => MainLobby.instance.GameStart());
    }
    private void Start()
    {
        MainLobby.instance.OnJoinedLobby += UpdateLobby_Event;
        MainLobby.instance.OnJoinedLobbyUpdate += UpdateLobby_Event;
        MainLobby.instance.OnLeftLobby += LobbyManager_OnLeftLobby;
        MainLobby.instance.OnKickedFromLobby += LobbyManager_OnLeftLobby;
                                       
        Hide();
    }

    private void LobbyManager_OnLeftLobby(object sender, System.EventArgs e)
    {
        ClearLobby();
        Hide();
    }
    private void UpdateLobby_Event(object sender, MainLobby.LobbyEventArgs e)
    {
        UpdateLobby();
    }
    private void UpdateLobby()
    {
        UpdateLobby(MainLobby.instance.GetJoinedLobby());
    }

    private void UpdateLobby(Lobby lobby)
    {
        ClearLobby();

        foreach(Player player in lobby.Players) {
            Transform playerSingleTransform = Instantiate(playerSingleTemplate, container);
            playerSingleTransform.gameObject.SetActive(true);
            PlayerManager lobbyPlayerSingleUI = playerSingleTransform.GetComponent<PlayerManager>();

            lobbyPlayerSingleUI.UpdatePlayer(player);
        }


        Show();
    }
    private void ClearLobby()
    {
        foreach (Transform child in container)
        {
            if (child == playerSingleTemplate) continue;
            Destroy(child.gameObject);
        }
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
        HostPanel();
    }
    public void HostPanel()
    {
        if (!MainLobby.instance.IsLobbyHost())
        {
            Message.instance.SetTitleAndMessageText(ExcelReader.instance.dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.YouAreNotHost)].name,
                ExcelReader.instance.dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.YouAreNotHost)].errorCode);
        }
    }
}
