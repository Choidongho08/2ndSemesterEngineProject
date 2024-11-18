using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class MultiplayerUI : MonoBehaviour
{
    [SerializeField] private Button hostBtn, joinBtn;

    private void Awake()
    {
        AssignInputs();
    }

    private void AssignInputs()
    {
        hostBtn.onClick.AddListener( delegate { NetworkManager.Singleton.StartHost(); } );
        joinBtn.onClick.AddListener( delegate { NetworkManager.Singleton.StartClient(); } );
    }
}
