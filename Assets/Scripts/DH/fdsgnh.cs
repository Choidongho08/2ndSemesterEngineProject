using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class fdsgnh : NetworkBehaviour
{
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private GameObject _lastChanceCanvas;

    private GameObject _chatManagerInstance;
    private NetworkObject _chatManagerNetworkObj;
    private GameObject _lastChanceInstance;
    private NetworkObject _lastChanceNetworkObj;

    private string _playerId;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public override void OnNetworkSpawn()
    {
        Debug.Log("OnNetWorkSpawn");
        if (!IsServer)
            return;

        _chatManagerInstance = Instantiate(_uiCanvas);

        _chatManagerNetworkObj = _chatManagerInstance.GetComponent<NetworkObject>();
        _chatManagerNetworkObj.Spawn();
        Vote.OnVoted += (() =>
        {
            StartCoroutine(Wait1sec());
        });
        ReturnVote.OnVoted += (() =>
        {
            StartCoroutine(Wait1secs());
        });
    }
    private IEnumerator Wait1sec()
    {
        Util.instance.LoadingShow();
        yield return new WaitForSeconds(1f);
            _chatManagerNetworkObj.Despawn();
        _lastChanceInstance = Instantiate(_lastChanceCanvas);
        _lastChanceNetworkObj = _lastChanceInstance.GetComponent<NetworkObject>();
        _lastChanceNetworkObj.Spawn();
        Util.instance.LoadingHide();
    }
    private IEnumerator Wait1secs()
    {
        Util.instance.LoadingShow();
        yield return new WaitForSeconds(1f);
        _lastChanceNetworkObj.Despawn();
        _chatManagerInstance = Instantiate(_uiCanvas);
        _chatManagerNetworkObj = _chatManagerInstance.GetComponent<NetworkObject>();
        _chatManagerNetworkObj.Spawn();
        Util.instance.LoadingHide();

    }
}
