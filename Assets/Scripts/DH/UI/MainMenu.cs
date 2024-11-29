using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _quickJoinLobbyButton;
    [SerializeField] private Button _joinLobbyByCodeButton;
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _changePlayerNameButton;
    [SerializeField] private GameObject _childGameObject;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private CreateLobby _createLobby;

    public event Action OnChangeName;
    public event Action OnJoinLobbyByCode;
    public event Action OnCreateLobby;


    private void Awake()
    {
        _changePlayerNameButton.onClick.AddListener(() => OnChangeName?.Invoke());
        _quickJoinLobbyButton.onClick.AddListener(() =>
        {
            MainLobby.instance.QuickJoinLobby();
            _childGameObject.SetActive(false);
            Util.instance.LoadingShow();
            DoScaleDown(_quickJoinLobbyButton.gameObject, _quickJoinLobbyButton.GetComponent<MouseEnterExitEvents>().objectLocalScale);
        });
        _joinLobbyByCodeButton.onClick.AddListener(() => 
        {
            OnJoinLobbyByCode?.Invoke();
            DoScaleDown(_joinLobbyByCodeButton.gameObject, _joinLobbyByCodeButton.GetComponent<MouseEnterExitEvents>().objectLocalScale);
        });
        _createLobbyButton.onClick.AddListener(() =>
        {
            _createLobby.gameObject.SetActive(true);
            NowCase.instance.ShowText();
            OnCreateLobby?.Invoke();
            _childGameObject.SetActive(false);
            DoScaleDown(_createLobbyButton.gameObject, _createLobbyButton.GetComponent<MouseEnterExitEvents>().objectLocalScale);
        });
        MainLobby.instance.OnAfterAuthenticate += () =>
        {
            Util.instance.LoadingHide();
            _childGameObject.SetActive(true);
        };
        _quickJoinLobbyButton.GetComponent<MouseEnterExitEvents>().OnEnterMouse += () => { DoScaleUp(_quickJoinLobbyButton.gameObject, _quickJoinLobbyButton.GetComponent<MouseEnterExitEvents>().objectLocalScale); };
        _quickJoinLobbyButton.GetComponent<MouseEnterExitEvents>().OnExitMouse += () => { DoScaleDown(_quickJoinLobbyButton.gameObject, _quickJoinLobbyButton.GetComponent<MouseEnterExitEvents>().objectLocalScale); };
        _joinLobbyByCodeButton.GetComponent<MouseEnterExitEvents>().OnEnterMouse += () => { DoScaleUp(_joinLobbyByCodeButton.gameObject, _joinLobbyByCodeButton.GetComponent<MouseEnterExitEvents>().objectLocalScale); };
        _joinLobbyByCodeButton.GetComponent<MouseEnterExitEvents>().OnExitMouse += () => { DoScaleDown(_joinLobbyByCodeButton.gameObject, _joinLobbyByCodeButton.GetComponent<MouseEnterExitEvents>().objectLocalScale); };
        _createLobbyButton.GetComponent<MouseEnterExitEvents>().OnEnterMouse += () => { DoScaleUp(_createLobbyButton.gameObject, _createLobbyButton.GetComponent<MouseEnterExitEvents>().objectLocalScale); };
        _createLobbyButton.GetComponent<MouseEnterExitEvents>().OnExitMouse += () => { DoScaleDown(_createLobbyButton.gameObject, _createLobbyButton.GetComponent<MouseEnterExitEvents>().objectLocalScale); };
    }
    public void GetPlayerName(string playerName)
    {
        _playerName.text = playerName;
    }
    private void DoScaleUp(GameObject gameObject, Vector3 localScale)
    {
        gameObject.transform.DOScale(localScale + new Vector3(0.15f, 0.15f), 0.2f);
    }
    private void DoScaleDown(GameObject gameObject, Vector3 localScale)
    {
        gameObject.transform.DOScale(localScale, 0.2f);
    }
}
