using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobby : MonoBehaviour
{
    [SerializeField] private Button _createLobbyBtn;
    [SerializeField] private Transform _setLobbyOptionUI;

    private Button _lobbyAccessModifyBtn;

    private void Awake()
    {
        _lobbyAccessModifyBtn = _setLobbyOptionUI.GetComponentInChildren<Button>();
    }
    private void Start()
    {
        _createLobbyBtn.onClick.AddListener(() =>
        {
            _createLobbyBtn.gameObject.SetActive(false);
            _setLobbyOptionUI.gameObject.SetActive(true);
        });
        _lobbyAccessModifyBtn.onClick.AddListener(() => 
        {
            _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text =
            _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text == "Public" ? "Private" : "Public";
        });
    }
}
