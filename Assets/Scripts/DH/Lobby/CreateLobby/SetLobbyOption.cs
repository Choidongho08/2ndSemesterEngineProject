using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetLobbyOption : MonoBehaviour
{
    [SerializeField] private Button _lobbyAccessModifyBtn;

    public bool IsPrivate;
    private void Start()
    {
        _lobbyAccessModifyBtn.onClick.AddListener(() =>
        {
            _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text =
            _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text == "Public" ? "Private" : "Public";
            IsPrivate = _lobbyAccessModifyBtn.GetComponentInChildren<TextMeshProUGUI>().text == "Public" ? false : true;
        });
    }
}
