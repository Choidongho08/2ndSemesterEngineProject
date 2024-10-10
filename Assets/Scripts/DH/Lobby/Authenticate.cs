using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class Authenticate : MonoBehaviour
{
    [SerializeField] private Button authenticateBtn;
    public event Action OnAfterAuthenticate;

    private void Start()
    {
        authenticateBtn.gameObject.SetActive(true);
        authenticateBtn.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            AuthenticateAsync();
            OnAfterAuthenticate?.Invoke();
            authenticateBtn.gameObject.SetActive(false);
        });
    }

    private async void AuthenticateAsync()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
