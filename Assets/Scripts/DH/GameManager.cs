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
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MainLobby.instance.OnGameStart += GameStart;
        SceneManager.sceneLoaded += (a,b) => SceneChange();
    }

    private void GameStart()
    {
        GameStartServerRpc();
    }
    [ServerRpc]
    private void GameStartServerRpc()
    {
        GameStartClientRpc();
    }
    [ClientRpc]
    private void GameStartClientRpc()
    {
        NetworkManager.SceneManager.LoadScene($"Case{NowCase.instance.CaseNumber()}", LoadSceneMode.Single);
    }
    private void SceneChange()
    {
        _world1Canvas = GameObject.Find("World1Canvas").GetComponent<Canvas>();
        _world2Canvas = GameObject.Find("World2Canvas").GetComponent<Canvas>();
        _world1BackGroundImgManager = GameObject.Find("World1ChangeImgManager");
        _world2BackGroundImgManager = GameObject.Find("World2ChangeImgManager");
        if (IsHost)
        {
            obj.NetworkObject.Spawn();
            _world1Canvas.gameObject.SetActive(true);
            _world2Canvas.gameObject.SetActive(false);
            _world2BackGroundImgManager.gameObject.SetActive(false);
        }
        else if(IsClient)
        {
            _world2Canvas.gameObject.SetActive(true);
            _world1Canvas.gameObject.SetActive(false);
            _world1BackGroundImgManager .gameObject.SetActive(false);
        }
    }

}
