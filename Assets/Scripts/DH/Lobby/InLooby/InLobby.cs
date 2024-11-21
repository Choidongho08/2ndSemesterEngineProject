using TMPro;
using UnityEngine;

public class InLobby : MonoBehaviour
{
    [SerializeField] private GameObject _lobby;
    [SerializeField] private TextMeshProUGUI _lobbyCode;
    [SerializeField] private TextMeshProUGUI _lobbyName;
    [SerializeField] private TextMeshProUGUI _lobbyCase;

    private void Awake()
    {
        MainLobby.instance.OnLobbyCreate += (lobbyCode, lobbyName, lobbyCase) => Lobby(lobbyCode, lobbyName, lobbyCase);
        MainLobby.instance.OnLobbyJoined += (lobbyCode, lobbyName, lobbyCase) => Lobby(lobbyCode, lobbyName, lobbyCase);
    }
    private void Lobby(string lobbyCode, string lobbyName, string lobbyCase)
    {
        _lobbyCode.text = lobbyCode;
        _lobbyName.text = lobbyName;
        _lobbyCase.text = lobbyCase;
        _lobby.SetActive(true);
        Util.instance.LoadingHide();
    }
}
