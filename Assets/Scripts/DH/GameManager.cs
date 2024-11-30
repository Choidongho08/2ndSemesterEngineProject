using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MainLobby.instance.OnGameStart += GameStart;
    }

    private void GameStart()
    {
        if (MainLobby.instance.IsLobbyHost())
            SceneManager.LoadScene($"Case{NowCase.instance.CaseNumber()}World1");
        else if (!MainLobby.instance.IsLobbyHost())
            SceneManager.LoadScene($"Case{NowCase.instance.CaseNumber()}World2");
    }
}
