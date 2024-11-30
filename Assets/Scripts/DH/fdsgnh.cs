using System;
using Unity.Multiplayer.Samples.Utilities;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fdsgnh : NetworkBehaviour
{
    [SerializeField] private GameObject _uiCanvas;

    private GameObject _chatManagerInstance;
    private NetworkObject _chatManagerNetworkObj;

    private string _playerId;

    public override void OnNetworkDespawn()
    {
        if (!IsServer)
            return;

        _chatManagerInstance = Instantiate(_uiCanvas);

        _chatManagerNetworkObj = _chatManagerInstance.GetComponent<NetworkObject>();
        _chatManagerNetworkObj.Spawn();
    }
}
