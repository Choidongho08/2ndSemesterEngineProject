using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    private Canvas _world1Canvas, _world2Canvas;
    private GameObject _world1BackGroundImgManager, _world2BackGroundImgManager;
    private bool _isHost;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MainLobby.instance.OnGameStart += GameStart;
    }
    private void GameStart()
    {
        _isHost = MainLobby.instance.IsLobbyHost();
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
        SceneChange();
    }
    private void SceneChange()
    {
        _world1Canvas = GameObject.Find("World1Canvas").GetComponent<Canvas>();
        _world2Canvas = GameObject.Find("World2Canvas").GetComponent<Canvas>();
        _world1BackGroundImgManager = GameObject.Find("World1ChangeImgManager");
        _world2BackGroundImgManager = GameObject.Find("World2ChangeImgManager");
        if (IsHost)
        {
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
