using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InLobbyUI : MonoSingleton<InLobbyUI>
{
    [SerializeField] private Transform playerSingleTemplate;
    [SerializeField] private Transform container;
    [SerializeField] private Button _readyButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button leaveLobbyButton;
    [SerializeField] private PlayerSO _playerSo;
    [SerializeField] private CreateLobby _createLobby;

    private InLobby _inLobby;



    private void Awake()
    {
        _inLobby = GetComponentInParent<InLobby>();
        playerSingleTemplate.gameObject.SetActive(false);

        leaveLobbyButton.onClick.AddListener(() =>
        {
            _createLobby.ClearCreateLobbyOption();
            Util.instance.LoadingShow();
            MainLobby.instance.LeaveLobby();
            NowCase.instance.HideText();
            gameObject.SetActive(false);
        });
        _readyButton.onClick.AddListener(() =>
        {
            _playerSo.playerReady = MainLobby.instance.PlayerReady == "False" ? "True" : "False";
            MainLobby.instance.UpdatePlayerReady(_playerSo.playerReady);
        });
        _startButton.onClick.AddListener(() =>
        {
            MainLobby.instance.GameStart();
        });
    }
    private void Start()
    {
        MainLobby.instance.OnJoinedLobbyUpdate += UpdateLobby_Event;
        MainLobby.instance.OnLeftLobby += LobbyManager_OnLeftLobby;
        MainLobby.instance.OnKickedFromLobby += LobbyManager_OnLeftLobby;
    }
    private void OnEnable()
    {
        StartButton();
    }

    private void StartButton()
    {
        if (MainLobby.instance.IsLobbyHost())
            _startButton.gameObject.SetActive(true);
        else
            _startButton.gameObject.SetActive(false);
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

        foreach (Player player in lobby.Players)
        {
            Transform playerSingleTransform = Instantiate(playerSingleTemplate, container);
            playerSingleTransform.gameObject.SetActive(true);
            PlayerManager lobbyPlayerSingleUI = playerSingleTransform.GetComponent<PlayerManager>();

            lobbyPlayerSingleUI.UpdatePlayer(player);
        }

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
}
