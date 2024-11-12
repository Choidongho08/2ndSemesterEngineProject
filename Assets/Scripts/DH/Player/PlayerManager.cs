using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Image _playerReadyImage;
    [SerializeField] private Image _playerIsHost;

    private Player player;


    public void UpdatePlayer(Player player)
    {
        this.player = player;
        playerNameText.text = player.Data[MainLobby.instance.KeyPlayerName].Value;
        if (MainLobby.instance.IsLobbyHost())
            _playerIsHost.gameObject.SetActive(true);
        else
            _playerIsHost.gameObject.SetActive(false);
        MainLobby.PlayerReadyEnum playerReady = System.Enum.Parse<MainLobby.PlayerReadyEnum>(player.Data[MainLobby.instance.KeyPlayerReady].Value);
        _playerReadyImage.sprite = LobbyAsset.instance.GetSprite(playerReady);
    }


}
