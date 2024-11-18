using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoSingleton<Message>
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Button _okayButton;
    [SerializeField] private GameObject _child;

    private void Awake()
    {
        _okayButton.onClick.AddListener(HideMessage);
    }

    public void SetTitleAndMessageText(string titleText, string messageText)
    {
        ShowMessage();
        _titleText.text = titleText;
        _messageText.text = messageText;
    }
    private void ShowMessage()
    {
        _child.SetActive(true);
    }
    private void HideMessage()
    {
        _child.SetActive(false);
    }
}
