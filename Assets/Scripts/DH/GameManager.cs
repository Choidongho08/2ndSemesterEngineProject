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
        {
            SceneManager.LoadScene("UI");
        }
        else
        {
            SceneManager.LoadScene("UI");
        }
    }
}
