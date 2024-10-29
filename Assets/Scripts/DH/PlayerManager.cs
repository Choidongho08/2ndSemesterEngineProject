using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Image _playerReadyImage;

    private Player player;
    private string _playerReady;


    public void UpdatePlayer(Player player)
    {
        this.player = player;
        playerNameText.text = player.Data[MainLobby.instance.KeyPlayerName].Value;
        MainLobby.PlayerReadyEnum playerReady = System.Enum.Parse<MainLobby.PlayerReadyEnum>(player.Data[MainLobby.instance.KeyPlayerReady].Value);
        _playerReadyImage.sprite = LobbyAsset.instance.GetSprite(playerReady);
    }

    
}
