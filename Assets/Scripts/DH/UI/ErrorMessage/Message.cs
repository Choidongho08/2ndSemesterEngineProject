using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoSingleton<Message>
{
    [SerializeField] private GameObject _top;
    [SerializeField] private GameObject _detail;

    private Button _okayButton;
    private TextMeshProUGUI _topTitleText;
    private TextMeshProUGUI _detailMessageText;

    private void Awake()
    {
        _okayButton = GetComponentInChildren<Button>();
        _okayButton.onClick.AddListener(() => gameObject.SetActive(false));
        _topTitleText = _top.GetComponentInChildren<TextMeshProUGUI>();
        _detailMessageText = _detail.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTitleAndMessageText(string titleText, string messageText)
    {
        _topTitleText.text = titleText;
        _detailMessageText.text = messageText;
        ShowMessage();
    }
    private void ShowMessage()
    {
        gameObject.SetActive(true);
    }
}
