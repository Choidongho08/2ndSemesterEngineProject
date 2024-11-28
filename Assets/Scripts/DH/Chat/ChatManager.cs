using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : NetworkBehaviour
{
    [SerializeField] private ChatMessage _chatMessagePrefab;
    [SerializeField] private GameObject _chatContent;
    [SerializeField] private TMP_InputField _chatInput;
    [SerializeField] private int _maxChatMessage;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private Button _chatButton;
    [SerializeField] private Image _chatOpenImage;
    [SerializeField] private RectTransform _chat;


    private List<ChatMessage> _chatMessageList = new List<ChatMessage>();
    private bool _chatOpen = false;

    public static ChatManager Singleton;
    public string playerName;

    private void Awake()
    {
        ChatManager.Singleton = this;
        _chatButton.onClick.AddListener(() =>
        {
            if (_chatOpen)
            {
                _chat.DOLocalMoveY(-311f, 0.3f);
                _chatOpenImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                _chatOpen = false;
            }
            else
            {
                _chat.transform.DOLocalMoveY(-132f, 0.3f);
                _chatOpenImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                _chatOpen = true;
            }
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage(_chatInput.text, _playerSO.playerName); 
            _chatInput.text = "";
        }
    }

    public void SendChatMessage(string _message, string _fromWho = null)
    {
        if (string.IsNullOrWhiteSpace(_message)) return;

        string S = _fromWho + " > " + _message;
        SendChatMessageServerRpc(S);
    }
    private void AddMessage(string msg)
    {
        Debug.Log(msg);
        Debug.Log(_chatContent);
        ChatMessage CM = Instantiate(_chatMessagePrefab, _chatContent.transform);
        CM.SetText(msg);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendChatMessageServerRpc(string message, ServerRpcParams serverRpcParams = default)
    {
        ReceiveChatMessageClientRpc(message);
    }
    [ClientRpc]
    private void ReceiveChatMessageClientRpc(string message, ClientRpcParams clientRpcParams = default)
    { //FixedString128Bytes
        AddMessage(message);
    }
}
