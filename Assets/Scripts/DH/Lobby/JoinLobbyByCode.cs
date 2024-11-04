using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyByCode : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputLobbyCode;
    [SerializeField] private Button _joinButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private GameObject _child;

    private string _lobbyCode;

    private void Awake()
    {
        _mainMenu.OnJoinLobbyByCode += () => _child.SetActive(true);
        _joinButton.onClick.AddListener(() =>
        {
            _inputLobbyCode.text = string.Empty;
            _lobbyCode = _inputLobbyCode.text;
            CodeJoinLobby();
            Hide();
        });
        _cancelButton.onClick.AddListener(() => { Hide(); });
    }

    private void Hide()
    {
        _child.SetActive(false);
    }
    private void CodeJoinLobby()
    {
        Util.instance.LoadingShow();
        MainLobby.instance.JoinLobbyByCode(_lobbyCode);
    }
}
