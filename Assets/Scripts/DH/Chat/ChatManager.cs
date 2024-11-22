using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class ChatManager : NetworkBehaviour
{
    [SerializeField] private ChatMessage chatMessagePrefab;
    [SerializeField] private CanvasGroup chatContent;
    [SerializeField] private TMP_InputField chatInput;
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
                _chat.DOMoveY(-282f, 0.3f);
                _chatOpenImage.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                _chatOpen = false;
            }
            else
            {
                _chat.transform.DOMoveY(303f, 0.3f);
                _chatOpenImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                _chatOpen = true;
            }
        });
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage(chatInput.text, _playerSO.playerName);
            chatInput.text = "";
        }
    }

    public void SendChatMessage(string _message, string _fromWho = null)
    { 
        if(string.IsNullOrWhiteSpace(_message)) return;

        string S = _fromWho + " > " +  _message;
        SendChatMessageServerRpc(S); 
    }
   
    private ChatMessage AddMessage(string msg)
    {
        ChatMessage CM = Instantiate(chatMessagePrefab, chatContent.transform);
        CM.SetText(msg);
        return CM;
    }
    private void ChatAddOnList(ChatMessage CM)
    {
        _chatMessageList.Add(CM);
        if (_chatMessageList.Count > _maxChatMessage)
        {
            List<ChatMessage> CMList = _chatMessageList.FindAll(chatMessage => chatMessage != null);
            _chatMessageList = CMList.ToList();
            Destroy(_chatMessageList[0].gameObject);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendChatMessageServerRpc(string message)
    {
        ReceiveChatMessageClientRpc(message);
    }

    [ClientRpc]
    private void ReceiveChatMessageClientRpc(string message)
    {
        ChatMessage CM = ChatManager.Singleton.AddMessage(message);
        ChatManager.Singleton.ChatAddOnList(CM);
    }
}
