using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    public void SetText(string str)
    {
        messageText.text = str; 
    }
}
