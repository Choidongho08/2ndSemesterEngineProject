using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Linq;

public class ChatManager : NetworkBehaviour
{
    [SerializeField] private ChatMessage chatMessagePrefab;
    [SerializeField] private CanvasGroup chatContent;
    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private int _maxChatMessage;
    [SerializeField] private PlayerSO _playerSO;

    List<ChatMessage> _chatMessageList = new List<ChatMessage>();

    public static ChatManager Singleton;
    public string playerName;



    private void Awake() 
    { ChatManager.Singleton = this; }

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
        if (_chatMessageList.Count >= _maxChatMessage)
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
