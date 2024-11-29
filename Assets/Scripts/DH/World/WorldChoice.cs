using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldChoice : NetworkBehaviour
{
    [SerializeField] private Button _world1Button, _world2Button;
    [SerializeField] private Image _world1Lock, _world2Lock;
    [SerializeField] private Image _world1LockIsOwner, _world2LockIsOwner;
    [SerializeField] private WorldSO _world1SO, _world2SO;
    [SerializeField] private TextMeshProUGUI _countDownText;

    public static int caseNumber;

    private void Awake()
    {
        _world1Button.onClick.AddListener(World1);
        _world2Button.onClick.AddListener(World2);
    }

    private void World1()
    {
        _world1SO.isOwner = _world1SO.isOwner ? true : false;
        _world1LockIsOwner.gameObject.SetActive(true);
        WorldChoiceServerRpc(_world1SO.worldName);
    }
    private void World2()
    {
        _world2SO.isOwner = _world2SO.isOwner ? true : false;
        _world2LockIsOwner.gameObject.SetActive(true);
        WorldChoiceServerRpc(_world1SO.worldName);
    }
    private void WorldLock(string name)
    {
        if(_world1SO.worldName == name)
        {
            _world1Lock.gameObject.SetActive(true);
        }
        if (_world2SO.worldName == name)
        {
            _world2Lock.gameObject.SetActive(true);
        }
    }
    [ServerRpc]
    private void WorldChoiceServerRpc(string name)
    {
        WorldChoiceClientRpc(_world2SO.worldName);
    }
    [ClientRpc]
    private void WorldChoiceClientRpc(string name)
    {
        WorldLock(name);
    }
}
