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
            StartGameServerRpc();
        }
        else
        {
            Message.instance.SetTitleAndMessageText(ExcelReader.instance.dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.YouAreNotHost)].name, ExcelReader.instance.dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.YouAreNotHost)].errorCode);
            return;
        }
    }
    [ServerRpc]
    private void StartGameServerRpc()
    {
        StartGameClientRpc();
    }
    [ClientRpc]
    private void StartGameClientRpc()
    {
        SceneManager.LoadScene(2);
    }
}
