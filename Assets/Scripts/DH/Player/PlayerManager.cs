using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Image _playerReadyYes;
    [SerializeField] private Image _playerReadyNo;

    private Player player;


    public void UpdatePlayer(Player player)
    {
        this.player = player;
        playerNameText.text = player.Data[MainLobby.instance.KeyPlayerName].Value;
        MainLobby.PlayerReadyEnum playerReady = System.Enum.Parse<MainLobby.PlayerReadyEnum>(player.Data[MainLobby.instance.KeyPlayerReady].Value);
        if(LobbyAsset.instance.GetSprite(playerReady) == "True" ? true : false)
        {
            _playerReadyNo.gameObject.SetActive(false);
            _playerReadyYes.gameObject.SetActive(true);
        }
        else
        {
            _playerReadyYes.gameObject.SetActive(false);
            _playerReadyNo.gameObject.SetActive(true);
        }
    }


}
