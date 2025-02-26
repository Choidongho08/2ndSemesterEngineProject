using TMPro;
using Unity.Netcode;
using UnityEngine;

public class InLobby : MonoBehaviour
{
    [SerializeField] private GameObject _lobby;
    [SerializeField] private TextMeshProUGUI _lobbyCode;
    [SerializeField] private TextMeshProUGUI _lobbyName;
    [SerializeField] private TextMeshProUGUI _lobbyCase;
    [SerializeField] private CaseBookListSO _caseBookListSo;

    private string _caseNumber;

    private void Awake()
    {
        MainLobby.instance.OnLobbyCreate += (lobbyCode, lobbyName, lobbyCase) => Lobby(lobbyCode, lobbyName, lobbyCase);
        MainLobby.instance.OnLobbyJoined += (lobbyCode, lobbyName, lobbyCase) => Lobby(lobbyCode, lobbyName, lobbyCase);
    }
    private void Lobby(string lobbyCode, string lobbyName, string lobbyCase)
    {
        _caseNumber = lobbyCase.Substring(4,1);
        NowCase.instance.SetCaseNumber(_caseNumber);
        _lobbyCode.text = lobbyCode;
        _lobbyName.text = lobbyName;
        if(lobbyCase == "Case1")
        {
            _lobbyCase.text = _caseBookListSo.list[0].caseName;
        }
        if (lobbyCase == "Case2")
        {
            _lobbyCase.text = _caseBookListSo.list[1].caseName;
        }
        _lobby.SetActive(true);
        Util.instance.LoadingHide();
    }
}
