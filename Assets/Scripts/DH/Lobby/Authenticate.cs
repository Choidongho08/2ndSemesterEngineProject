using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class Authenticate : MonoBehaviour
{
    [SerializeField] private Button authenticateBtn;

    private async void Start()
    {
        authenticateBtn.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            AuthenticateAsync();
            Hide();
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
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
