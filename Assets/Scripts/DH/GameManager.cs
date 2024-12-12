using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    private Canvas _world1Canvas, _world2Canvas;
    private GameObject _world1BackGroundImgManager, _world2BackGroundImgManager;
    private fdsgnh obj;
    private string _joinCode;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        MainLobby.instance.OnGameStart += GServerRpc;
    }
    private void GS()
    {
        GServerRpc();

        //if (IsHost)
        //{
        //    GServerRpc();
        //}

        //if (GameObject.Find("World1Canvas").TryGetComponent(out Canvas canvas)) return;
        //else
        //{
        //    _world1Canvas = GameObject.Find("World1Canvas").GetComponent<Canvas>();
        //    _world1BackGroundImgManager = GameObject.Find("World1ChangeImgManager");
        //    _world2Canvas = GameObject.Find("World2Canvas").GetComponent<Canvas>();
        //    _world2BackGroundImgManager = GameObject.Find("World2ChangeImgManager");
        //    if (IsHost)
        //    {
        //        obj.NetworkObject.Spawn();
        //        _world2Canvas.gameObject.SetActive(true);
        //        _world1Canvas.gameObject.SetActive(false);
        //        _world1BackGroundImgManager.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        _world1Canvas.gameObject.SetActive(true);
        //        _world2Canvas.gameObject.SetActive(false);
        //        _world2BackGroundImgManager.gameObject.SetActive(false);
        //    }
        //}
        //if (NetworkManager.Singleton.IsConnectedClient)
        //{
        //    Debug.LogError("Client is connected.");
        //}
        //else
        //{
        //    Debug.LogError("Client failed to connect to the server.");
        //}
    }
    [ServerRpc]
    private void GServerRpc()
    {
        GClientRpc();
    }
    [ClientRpc]
    private void GClientRpc()
    {
        GameStart();
        //NetworkManager.SceneManager.LoadScene($"Case{NowCase.instance.CaseNumber()}", LoadSceneMode.Single);
        //obj = Instantiate(GameObject.Find("GameObject").GetComponent<fdsgnh>());
        //Util.instance.LoadingHide();
    }
    private async void GameStart()
    {
        if (MainLobby.instance.IsLobbyHost())
        {
            _joinCode = await MainRelay.instance.CreateRelay();
        }
        else
        {
            await MainRelay.instance.JoinCodeRelay(_joinCode);
        }
    }
}
