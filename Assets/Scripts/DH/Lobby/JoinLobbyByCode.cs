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


    private void Awake()
    {
        _mainMenu.OnJoinLobbyByCode += () => _child.SetActive(true);
        _joinButton.onClick.AddListener(() =>
        {
            CodeJoinLobby(_inputLobbyCode.text);
            _inputLobbyCode.text = string.Empty;
            Hide();
        });
        _cancelButton.onClick.AddListener(() => { Hide(); });
    }

    private void Hide()
    {
        _child.SetActive(false);
    }
    private void CodeJoinLobby(string lobbyCode)
    {
        Util.instance.LoadingShow();
        MainLobby.instance.JoinLobbyByCode(lobbyCode);
    }
}
