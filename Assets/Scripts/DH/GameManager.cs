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

    private void Awake()
    {
        DontDestroyOnLoad(this);
        MainLobby.instance.OnGameStart += GameStart;
        SceneManager.sceneLoaded += Gamestart;
    }
    private void GameStart()
    {
        NetworkManager.SceneManager.LoadScene($"Case{NowCase.instance.CaseNumber()}", LoadSceneMode.Single);
        if (NetworkManager.Singleton.IsConnectedClient)
        {
            Debug.Log("Client is connected.");
        }
        else
        {
            Debug.Log("Client failed to connect to the server.");
        }
    }
    private void Gamestart(Scene scene, LoadSceneMode mode)
    {
        if (!GameObject.Find("World1Canvas").TryGetComponent(out Canvas canvas)) return;
        else
        {
            GameStartServerRpc();
            _world1Canvas = GameObject.Find("World1Canvas").GetComponent<Canvas>();
            _world1BackGroundImgManager = GameObject.Find("World1ChangeImgManager");
            _world2Canvas = GameObject.Find("World2Canvas").GetComponent<Canvas>();
            _world2BackGroundImgManager = GameObject.Find("World2ChangeImgManager");
            if (IsHost)
            {
                _world2Canvas.gameObject.SetActive(true);
                _world1Canvas.gameObject.SetActive(false);
                _world1BackGroundImgManager.gameObject.SetActive(false);
            }
            else if (IsClient)
            {
                _world1Canvas.gameObject.SetActive(true);
                _world2Canvas.gameObject.SetActive(false);
                _world2BackGroundImgManager.gameObject.SetActive(false);
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void GameStartServerRpc()
    {
        GameStartClientRpc();
    }
    [ClientRpc]
    private void GameStartClientRpc()
    {
        fdsgnh obj = Instantiate(GameObject.Find("GameObject").GetComponent<fdsgnh>());
        obj.NetworkObject.Spawn();
        Util.instance.LoadingHide();
    }
}
