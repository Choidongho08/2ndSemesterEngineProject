using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    private string playerName = string.Empty;
    private Lobby joinedLobby;

    [SerializeField] private TMP_InputField _inputField;
    //[SerializeField] private Autenticate autenticate;


    private void Start()
    {
        playerName = "Player" + Random.Range(0, 99);
        Debug.Log(playerName);
        _inputField.onValueChanged.AddListener(UpdatePlayerName);
    }

    private async void UpdatePlayerName(string newPlayerName)
    {
        try
        {
            playerName = newPlayerName;
            Debug.Log(playerName);
            await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
            {
                Data = new Dictionary<string, PlayerDataObject>
            {
                {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) }
            }
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
