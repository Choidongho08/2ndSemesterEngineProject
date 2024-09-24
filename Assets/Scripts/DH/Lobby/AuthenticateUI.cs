using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Authen : MonoBehaviour
{
    private Button authenticateBtn;

    private void Awake()
    {
        gameObject.SetActive(true);
        authenticateBtn = GetComponentInChildren<Button>();
        authenticateBtn.onClick.AddListener(() =>
        {
            LobbyManager.Instance.Authenticate(EditPlayerName2.Instance.GetPlayerName());
        });
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
