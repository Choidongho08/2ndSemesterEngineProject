using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class fdsgnh : MonoBehaviour
{
    [SerializeField] private FindCriminal _findCriminal;
    [SerializeField] private ChatManager _chatManager;

    private string _playerId;

    private void Start()
    {
        var findCriminal = Instantiate(_findCriminal);
        var chatManager = Instantiate(_chatManager);

        findCriminal.gameObject.SetActive(true);
        chatManager.gameObject.SetActive(true);

        findCriminal.GetComponent<NetworkObject>().Spawn();
        chatManager.GetComponent<NetworkObject>().Spawn();
    }
}
